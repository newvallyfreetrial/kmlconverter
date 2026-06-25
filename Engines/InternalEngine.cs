using System.Globalization;
using System.IO;
using System.Text;
using OSGeo.GDAL;
using GISUniversalConverterPro.Converters;
using GISUniversalConverterPro.Interfaces;
using GISUniversalConverterPro.Models;
using GISUniversalConverterPro.Services;
using MaxRev.Gdal.Core;
using OSGeo.OGR;
using OSGeo.OSR;

namespace GISUniversalConverterPro.Engines
{
    /// <summary>
    /// Converts KML/KMZ inputs using the built-in GDAL/OGR engine.
    /// </summary>
    public sealed class InternalEngine : IConversionEngine
    {
        private CancellationToken _cancellationToken;
        private bool _cancelRequested;
        private LoggingService? _loggingService;

        public string Name => "Internal Conversion Engine";
        public bool IsAvailable => true;
        public ConversionJob? Job { get; set; }
        public Action<int, string>? ProgressReporter { get; set; }
        public LoggingService? LoggingService
        {
            get => _loggingService;
            set => _loggingService = value;
        }

        public void Initialize()
        {
            try
            {
                GdalBase.ConfigureAll();
                Gdal.SetConfigOption("SHAPE_ENCODING", "UTF-8");
                Gdal.SetConfigOption("OGR_FORCE_ASCII", "NO");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to initialize the bundled GDAL runtime. Verify that the GDAL runtime package is deployed for win-x64.", ex);
            }
        }

        public void Convert()
        {
            if (Job is null)
            {
                throw new InvalidOperationException("No conversion job is assigned to the internal engine.");
            }

            if (!Validate())
            {
                throw new InvalidOperationException("The conversion job is invalid.");
            }

            ReportProgress(5, "Initializing GDAL/OGR engine...");
            if (_cancelRequested || _cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException(_cancellationToken);
            }

            var inputPath = Path.GetFullPath(Job.FullPath);
            var outputRoot = OutputService.EnsureWritableOutputDirectory(Job.OutputDirectory);
            var safeName = SanitizeFileName(Path.GetFileNameWithoutExtension(Job.FullPath));
            var outputDirectory = Path.Combine(outputRoot, safeName);
            Directory.CreateDirectory(outputDirectory);

            ReportProgress(10, $"Reading input file: {Job.FileName}");
            if (_cancelRequested || _cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException(_cancellationToken);
            }

            using var dataSource = Ogr.Open(inputPath, 0);
            if (dataSource is null)
            {
                throw new InvalidDataException($"Unable to read input data source: {Job.FullPath}");
            }

            var layerCount = dataSource.GetLayerCount();
            LogDiagnostic($"Opened KML datasource: {inputPath}; LayerCount={layerCount}");
            for (var index = 0; index < layerCount; index++)
            {
                if (_cancelRequested || _cancellationToken.IsCancellationRequested)
                {
                    throw new OperationCanceledException(_cancellationToken);
                }

                using var layer = dataSource.GetLayerByIndex(index);
                if (layer is null)
                {
                    continue;
                }

                var layerName = string.IsNullOrWhiteSpace(layer.GetName()) ? $"layer_{index + 1}" : layer.GetName();
                LogLayerSample(layer, $"After opening datasource, layer {index + 1}/{layerCount} ({layerName})");
                var outputBaseName = Path.Combine(outputDirectory, $"{safeName}_{layerName}");

                WriteShapefile(layer, outputBaseName);
                WriteGeoPackage(layer, outputBaseName);
                WriteGeoJson(layer, outputBaseName);

                ReportProgress(10 + ((index + 1) * 80 / Math.Max(1, layerCount)), $"Completed layer {layerName}");
            }

            ReportProgress(100, "Conversion completed successfully.");
        }

        public bool Validate()
        {
            if (Job is null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(Job.FullPath) || !File.Exists(Job.FullPath))
            {
                return false;
            }

            var extension = Path.GetExtension(Job.FullPath).ToLowerInvariant();
            if (extension != ".kml" && extension != ".kmz")
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(Job.OutputDirectory))
            {
                return false;
            }

            return true;
        }

        public void Cancel()
        {
            _cancelRequested = true;
        }

        public void SetCancellationToken(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            _cancelRequested = false;
        }

        private static string SanitizeFileName(string value)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            var sanitized = new string(value.Select(ch => invalidChars.Contains(ch) ? '_' : ch).ToArray());
            return string.IsNullOrWhiteSpace(sanitized) ? "conversion" : sanitized;
        }

        private void ReportProgress(int percent, string message)
        {
            ProgressReporter?.Invoke(percent, message);
        }

        private void LogDiagnostic(string message)
        {
            _loggingService?.Log($"[InternalEngine UTF-8 Trace] {message}");
        }

        private void WriteShapefile(Layer layer, string outputBaseName)
        {
            var outputPath = $"{outputBaseName}.shp";
            var driver = Ogr.GetDriverByName("ESRI Shapefile");
            using var outputDataSource = driver.CreateDataSource(outputPath, Array.Empty<string>());
            using var outputLayer = outputDataSource.CreateLayer(Path.GetFileNameWithoutExtension(outputPath), CreateSpatialReference(), layer.GetGeomType(), new[] { "ENCODING=UTF-8" });
            CopyLayerSchema(layer, outputLayer, LogDiagnostic);
            CopyFeatures(layer, outputLayer, LogDiagnostic);
        }

        private void WriteGeoPackage(Layer layer, string outputBaseName)
        {
            var outputPath = $"{outputBaseName}.gpkg";
            var driver = Ogr.GetDriverByName("GPKG");
            using var outputDataSource = driver.CreateDataSource(outputPath, Array.Empty<string>());
            using var outputLayer = outputDataSource.CreateLayer(Path.GetFileNameWithoutExtension(outputPath), CreateSpatialReference(), layer.GetGeomType(), null);
            CopyLayerSchema(layer, outputLayer, LogDiagnostic);
            CopyFeatures(layer, outputLayer, LogDiagnostic);
        }

        private void WriteGeoJson(Layer layer, string outputBaseName)
        {
            var outputPath = $"{outputBaseName}.geojson";
            var driver = Ogr.GetDriverByName("GeoJSON");
            using var outputDataSource = driver.CreateDataSource(outputPath, Array.Empty<string>());
            using var outputLayer = outputDataSource.CreateLayer(Path.GetFileNameWithoutExtension(outputPath), CreateSpatialReference(), layer.GetGeomType(), null);
            CopyLayerSchema(layer, outputLayer, LogDiagnostic);
            CopyFeatures(layer, outputLayer, LogDiagnostic);
        }

        private static SpatialReference CreateSpatialReference()
        {
            var spatialReference = new SpatialReference(string.Empty);
            spatialReference.ImportFromEPSG(4326);
            return spatialReference;
        }

        private static void CopyLayerSchema(Layer sourceLayer, Layer targetLayer, Action<string> logDiagnostic)
        {
            var sourceDefinition = sourceLayer.GetLayerDefn();
            for (var index = 0; index < sourceDefinition.GetFieldCount(); index++)
            {
                var sourceFieldDefinition = sourceDefinition.GetFieldDefn(index);
                using var targetFieldDefinition = new FieldDefn(sourceFieldDefinition.GetName(), sourceFieldDefinition.GetFieldType());
                targetFieldDefinition.SetWidth(sourceFieldDefinition.GetWidth());
                targetFieldDefinition.SetPrecision(sourceFieldDefinition.GetPrecision());
                logDiagnostic($"Creating FieldDefn; Index={index}; Name={sourceFieldDefinition.GetName()}; Type={sourceFieldDefinition.GetFieldType()}; Width={sourceFieldDefinition.GetWidth()}; Precision={sourceFieldDefinition.GetPrecision()}");
                targetLayer.CreateField(targetFieldDefinition, 1);
            }
        }

        private static void CopyFeatures(Layer sourceLayer, Layer targetLayer, Action<string> logDiagnostic)
        {
            sourceLayer.ResetReading();
            var sourceLayerDefinition = sourceLayer.GetLayerDefn();
            var targetLayerDefinition = targetLayer.GetLayerDefn();

            while (true)
            {
                using var sourceFeature = sourceLayer.GetNextFeature();
                if (sourceFeature is null)
                {
                    break;
                }

                LogFeatureValues(sourceFeature, sourceLayerDefinition, "After reading source feature", logDiagnostic);

                using var targetFeature = new Feature(targetLayerDefinition);
                var geometry = sourceFeature.GetGeometryRef();
                if (geometry is not null)
                {
                    using var geometryClone = geometry.Clone();
                    targetFeature.SetGeometry(geometryClone);
                }

                LogFeatureValues(sourceFeature, sourceLayerDefinition, "Before creating output feature", logDiagnostic);
                for (var fieldIndex = 0; fieldIndex < sourceLayerDefinition.GetFieldCount(); fieldIndex++)
                {
                    var fieldDefinition = sourceLayerDefinition.GetFieldDefn(fieldIndex);
                    var fieldName = fieldDefinition.GetName();
                    var fieldValue = sourceFeature.GetFieldAsString(fieldIndex);
                    LogTextValue("After reading field value with Feature.GetFieldAsString()", sourceFeature.GetFID(), fieldName, fieldValue, logDiagnostic);
                    LogTextValue("Before Feature.SetField()", sourceFeature.GetFID(), fieldName, fieldValue, logDiagnostic);
                    targetFeature.SetField(fieldIndex, fieldValue);
                }

                LogFeatureValues(targetFeature, targetLayerDefinition, "Before Layer.CreateFeature()", logDiagnostic);
                targetLayer.CreateFeature(targetFeature);
            }
        }

        private static void LogLayerSample(Layer layer, string stage, Action<string> logDiagnostic)
        {
            layer.ResetReading();
            var definition = layer.GetLayerDefn();
            using var feature = layer.GetNextFeature();
            if (feature is not null)
            {
                LogFeatureValues(feature, definition, stage, logDiagnostic);
            }

            layer.ResetReading();
        }

        private void LogLayerSample(Layer layer, string stage)
        {
            LogLayerSample(layer, stage, LogDiagnostic);
        }

        private static void LogFeatureValues(Feature feature, FeatureDefn definition, string stage, Action<string> logDiagnostic)
        {
            for (var fieldIndex = 0; fieldIndex < definition.GetFieldCount(); fieldIndex++)
            {
                var fieldDefinition = definition.GetFieldDefn(fieldIndex);
                LogTextValue(stage, feature.GetFID(), fieldDefinition.GetName(), feature.GetFieldAsString(fieldIndex), logDiagnostic);
            }
        }

        private static void LogTextValue(string stage, long featureId, string fieldName, string? value, Action<string> logDiagnostic)
        {
            if (string.IsNullOrEmpty(value) || !ContainsNonAscii(value))
            {
                return;
            }

            var codePoints = string.Join(" ", value.EnumerateRunes().Select(rune => $"U+{rune.Value:X4}"));
            logDiagnostic(string.Create(CultureInfo.InvariantCulture, $"{stage}; FID={featureId}; Field={fieldName}; Value='{value}'; CodePoints={codePoints}"));
        }

        private static bool ContainsNonAscii(string value)
        {
            return value.Any(character => character > 0x7F);
        }
    }
}
