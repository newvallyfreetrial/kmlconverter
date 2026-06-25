using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GISUniversalConverterPro.UI;

namespace GISUniversalConverterPro
{
    partial class MainForm
    {
        private IContainer components = null;

        private MenuStrip mainMenuStrip;
        private ToolStrip mainToolStrip;
        private ToolStripButton addFilesToolStripButton;
        private ToolStripButton browseOutputToolStripButton;
        private ToolStripButton convertToolStripButton;
        private ToolStripButton cancelToolStripButton;
        private StaticGradientTableLayoutPanel rootLayoutPanel;
        private GradientRoundedPanel headerPanel;
        private Label titleLabel;
        private Label subtitleLabel;
        private TableLayoutPanel contentLayoutPanel;
        private RoundedPanel filesPanel;
        private RoundedPanel settingsPanel;
        private RoundedPanel progressPanel;
        private RoundedPanel logPanel;
        private Label filesHeaderLabel;
        private Label settingsHeaderLabel;
        private Label outputLabel;
        private TextBox outputPathTextBox;
        private FlowLayoutPanel fileActionPanel;
        private FlowLayoutPanel settingsActionPanel;
        private RoundedButton addFilesButton;
        private RoundedButton removeButton;
        private RoundedButton clearButton;
        private RoundedButton browseOutputButton;
        private RoundedButton convertButton;
        private RoundedButton cancelButton;
        private RoundedButton openOutputFolderButton;
        private RoundedButton toggleLogButton;
        private ListView filesListView;
        private ColumnHeader fileNameColumnHeader;
        private ColumnHeader sizeColumnHeader;
        private ColumnHeader statusColumnHeader;
        private ProgressBar progressBar;
        private Label progressTitleLabel;
        private RichTextBox logRichTextBox;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private ToolStripStatusLabel totalFilesStatusLabel;
        private ToolStripStatusLabel readyFilesStatusLabel;
        private TableLayoutPanel outputLayoutPanel;

        private ToolStripMenuItem fileMenuItem;
        private ToolStripMenuItem addFilesMenuItem;
        private ToolStripMenuItem browseOutputMenuItem;
        private ToolStripMenuItem openOutputFolderMenuItem;
        private ToolStripMenuItem convertMenuItem;
        private ToolStripMenuItem exitMenuItem;
        private ToolStripMenuItem toolsMenuItem;
        private ToolStripMenuItem helpMenuItem;
        private ToolStripMenuItem aboutMenuItem;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            mainMenuStrip = new MenuStrip();
            fileMenuItem = new ToolStripMenuItem();
            addFilesMenuItem = new ToolStripMenuItem();
            browseOutputMenuItem = new ToolStripMenuItem();
            openOutputFolderMenuItem = new ToolStripMenuItem();
            convertMenuItem = new ToolStripMenuItem();
            exitMenuItem = new ToolStripMenuItem();
            toolsMenuItem = new ToolStripMenuItem();
            helpMenuItem = new ToolStripMenuItem();
            aboutMenuItem = new ToolStripMenuItem();
            mainToolStrip = new ToolStrip();
            addFilesToolStripButton = new ToolStripButton();
            browseOutputToolStripButton = new ToolStripButton();
            convertToolStripButton = new ToolStripButton();
            cancelToolStripButton = new ToolStripButton();
            rootLayoutPanel = new StaticGradientTableLayoutPanel();
            headerPanel = new GradientRoundedPanel();
            titleLabel = new Label();
            subtitleLabel = new Label();
            contentLayoutPanel = new TableLayoutPanel();
            filesPanel = new RoundedPanel();
            settingsPanel = new RoundedPanel();
            progressPanel = new RoundedPanel();
            logPanel = new RoundedPanel();
            filesHeaderLabel = new Label();
            settingsHeaderLabel = new Label();
            outputLabel = new Label();
            outputPathTextBox = new TextBox();
            fileActionPanel = new FlowLayoutPanel();
            settingsActionPanel = new FlowLayoutPanel();
            addFilesButton = new RoundedButton();
            removeButton = new RoundedButton();
            clearButton = new RoundedButton();
            browseOutputButton = new RoundedButton();
            convertButton = new RoundedButton();
            cancelButton = new RoundedButton();
            openOutputFolderButton = new RoundedButton();
            toggleLogButton = new RoundedButton();
            filesListView = new ListView();
            fileNameColumnHeader = new ColumnHeader();
            sizeColumnHeader = new ColumnHeader();
            statusColumnHeader = new ColumnHeader();
            progressBar = new ProgressBar();
            progressTitleLabel = new Label();
            logRichTextBox = new RichTextBox();
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            totalFilesStatusLabel = new ToolStripStatusLabel();
            readyFilesStatusLabel = new ToolStripStatusLabel();
            outputLayoutPanel = new TableLayoutPanel();

            SuspendLayout();

            mainMenuStrip.BackColor = Color.FromArgb(246, 253, 255);
            mainMenuStrip.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            mainMenuStrip.Items.AddRange(new ToolStripItem[] { fileMenuItem, toolsMenuItem, helpMenuItem });
            mainMenuStrip.Dock = DockStyle.Top;
            mainMenuStrip.RightToLeft = RightToLeft.Yes;
            mainMenuStrip.RenderMode = ToolStripRenderMode.System;

            fileMenuItem.Text = "🗂️ ملف";
            fileMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addFilesMenuItem, browseOutputMenuItem, openOutputFolderMenuItem, new ToolStripSeparator(), convertMenuItem, new ToolStripSeparator(), exitMenuItem });
            addFilesMenuItem.Text = "➕ إضافة ملفات";
            addFilesMenuItem.Click += addFilesMenuItem_Click;
            browseOutputMenuItem.Text = "📁 تحديد مجلد الإخراج";
            browseOutputMenuItem.Click += browseOutputMenuItem_Click;
            openOutputFolderMenuItem.Text = "🗃️ فتح مجلد الإخراج";
            openOutputFolderMenuItem.Click += openOutputFolderMenuItem_Click;
            convertMenuItem.Text = "⚡ تحويل";
            convertMenuItem.Click += convertMenuItem_Click;
            exitMenuItem.Text = "🚪 خروج";
            exitMenuItem.Click += exitMenuItem_Click;
            toolsMenuItem.Text = "🧰 أدوات";
            helpMenuItem.Text = "❔ مساعدة";
            aboutMenuItem.Text = "ℹ️ حول التطبيق";
            helpMenuItem.DropDownItems.Add(aboutMenuItem);
            aboutMenuItem.Click += aboutMenuItem_Click;

            mainToolStrip.BackColor = Color.FromArgb(235, 250, 255);
            mainToolStrip.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            mainToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            mainToolStrip.Items.AddRange(new ToolStripItem[] { addFilesToolStripButton, browseOutputToolStripButton, convertToolStripButton, cancelToolStripButton });
            mainToolStrip.Dock = DockStyle.Top;
            mainToolStrip.RightToLeft = RightToLeft.Yes;
            addFilesToolStripButton.Text = "➕ إضافة ملفات";
            addFilesToolStripButton.Click += addFilesToolStripButton_Click;
            browseOutputToolStripButton.Text = "📁 الإخراج";
            browseOutputToolStripButton.Click += browseOutputToolStripButton_Click;
            convertToolStripButton.Text = "⚡ تحويل";
            convertToolStripButton.ForeColor = Color.FromArgb(7, 89, 133);
            convertToolStripButton.Click += convertToolStripButton_Click;
            cancelToolStripButton.Text = "⏹️ إلغاء";
            cancelToolStripButton.Click += cancelToolStripButton_Click;

            rootLayoutPanel.Dock = DockStyle.Fill;
            rootLayoutPanel.ColumnCount = 1;
            rootLayoutPanel.RowCount = 4;
            rootLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 98F));
            rootLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 170F));
            rootLayoutPanel.Padding = new Padding(18, 16, 18, 16);

            headerPanel.CornerRadius = 26;
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.Margin = new Padding(0, 0, 0, 14);
            headerPanel.Padding = new Padding(24, 16, 24, 16);
            titleLabel.Text = "GIS Universal Converter Pro";
            titleLabel.Dock = DockStyle.Top;
            titleLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point);
            titleLabel.ForeColor = Color.White;
            titleLabel.Height = 48;
            titleLabel.TextAlign = ContentAlignment.MiddleRight;
            subtitleLabel.Text = "لوحة تحويل GIS عصرية تدعم KML / KMZ بمحركات داخلية أو ArcGIS Pro";
            subtitleLabel.Dock = DockStyle.Top;
            subtitleLabel.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            subtitleLabel.ForeColor = Color.FromArgb(224, 247, 250);
            subtitleLabel.Height = 30;
            subtitleLabel.TextAlign = ContentAlignment.MiddleRight;
            headerPanel.Controls.Add(subtitleLabel);
            headerPanel.Controls.Add(titleLabel);

            contentLayoutPanel.BackColor = Color.FromArgb(224, 247, 250);
            contentLayoutPanel.Dock = DockStyle.Fill;
            contentLayoutPanel.ColumnCount = 2;
            contentLayoutPanel.RowCount = 1;
            contentLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62F));
            contentLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F));

            filesPanel.BackColor = Color.FromArgb(252, 254, 255);
            filesPanel.CornerRadius = 24;
            filesPanel.Dock = DockStyle.Fill;
            filesPanel.Margin = new Padding(0, 0, 8, 0);
            filesPanel.Padding = new Padding(18);
            filesHeaderLabel.Text = "🗺️ ملفات المصدر";
            filesHeaderLabel.Dock = DockStyle.Top;
            filesHeaderLabel.Height = 32;
            filesHeaderLabel.Font = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point);
            filesHeaderLabel.ForeColor = Color.FromArgb(12, 74, 110);
            filesHeaderLabel.TextAlign = ContentAlignment.MiddleRight;
            fileActionPanel.Dock = DockStyle.Bottom;
            fileActionPanel.Height = 54;
            fileActionPanel.FlowDirection = FlowDirection.RightToLeft;
            fileActionPanel.WrapContents = false;
            fileActionPanel.Padding = new Padding(0, 10, 0, 0);

            addFilesButton.Text = "➕ إضافة";
            addFilesButton.Size = new Size(116, 40);
            addFilesButton.Click += addFilesButton_Click;
            removeButton.Text = "🗑️ إزالة";
            removeButton.Size = new Size(116, 40);
            removeButton.Click += removeButton_Click;
            clearButton.Text = "🧹 مسح";
            clearButton.Size = new Size(100, 40);
            clearButton.Click += clearButton_Click;
            fileActionPanel.Controls.Add(addFilesButton);
            fileActionPanel.Controls.Add(removeButton);
            fileActionPanel.Controls.Add(clearButton);

            filesListView.Dock = DockStyle.Fill;
            filesListView.View = View.Details;
            filesListView.FullRowSelect = true;
            filesListView.GridLines = false;
            filesListView.MultiSelect = true;
            filesListView.HideSelection = false;
            filesListView.RightToLeft = RightToLeft.Yes;
            filesListView.RightToLeftLayout = true;
            filesListView.BorderStyle = BorderStyle.None;
            filesListView.BackColor = Color.FromArgb(247, 253, 255);
            filesListView.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            filesListView.Columns.AddRange(new[] { fileNameColumnHeader, sizeColumnHeader, statusColumnHeader });
            fileNameColumnHeader.Text = "اسم الملف";
            sizeColumnHeader.Text = "الحجم";
            statusColumnHeader.Text = "الحالة";
            fileNameColumnHeader.Width = 420;
            sizeColumnHeader.Width = 130;
            statusColumnHeader.Width = 140;
            filesPanel.Controls.Add(filesListView);
            filesPanel.Controls.Add(fileActionPanel);
            filesPanel.Controls.Add(filesHeaderLabel);

            settingsPanel.BackColor = Color.FromArgb(252, 254, 255);
            settingsPanel.CornerRadius = 24;
            settingsPanel.Dock = DockStyle.Fill;
            settingsPanel.Margin = new Padding(8, 0, 0, 0);
            settingsPanel.Padding = new Padding(18);
            settingsHeaderLabel.Text = "⚙️ إعدادات التحويل";
            settingsHeaderLabel.Dock = DockStyle.Top;
            settingsHeaderLabel.Height = 36;
            settingsHeaderLabel.Font = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point);
            settingsHeaderLabel.ForeColor = Color.FromArgb(12, 74, 110);
            settingsHeaderLabel.TextAlign = ContentAlignment.MiddleRight;

            outputLayoutPanel.Dock = DockStyle.Top;
            outputLayoutPanel.Height = 98;
            outputLayoutPanel.ColumnCount = 1;
            outputLayoutPanel.RowCount = 2;
            outputLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            outputLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            outputLabel.Text = "📁 مجلد الإخراج";
            outputLabel.Dock = DockStyle.Top;
            outputLabel.Height = 28;
            outputLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            outputLabel.ForeColor = Color.FromArgb(15, 118, 110);
            outputLabel.TextAlign = ContentAlignment.MiddleRight;
            outputPathTextBox.ReadOnly = true;
            outputPathTextBox.Dock = DockStyle.Top;
            outputPathTextBox.Height = 34;
            outputPathTextBox.BorderStyle = BorderStyle.FixedSingle;
            outputPathTextBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            outputLayoutPanel.Controls.Add(outputLabel, 0, 0);
            outputLayoutPanel.Controls.Add(outputPathTextBox, 0, 1);

            settingsActionPanel.Dock = DockStyle.Top;
            settingsActionPanel.Height = 190;
            settingsActionPanel.FlowDirection = FlowDirection.RightToLeft;
            settingsActionPanel.WrapContents = true;
            settingsActionPanel.Padding = new Padding(0, 14, 0, 0);
            browseOutputButton.Text = "📁 تحديد الإخراج";
            browseOutputButton.Size = new Size(170, 42);
            browseOutputButton.Click += browseOutputButton_Click;
            openOutputFolderButton.Text = "🗃️ فتح المجلد";
            openOutputFolderButton.Size = new Size(160, 42);
            openOutputFolderButton.Click += openOutputFolderButton_Click;
            convertButton.Text = "⚡ تحويل الآن";
            convertButton.Size = new Size(170, 48);
            convertButton.FillColor = ColorTranslator.FromHtml("#14B8A6");
            convertButton.HoverColor = ColorTranslator.FromHtml("#38BDF8");
            convertButton.PressedColor = Color.FromArgb(8, 145, 132);
            convertButton.TextColor = Color.White;
            convertButton.Click += convertButton_Click;
            cancelButton.Text = "⏹️ إلغاء";
            cancelButton.Size = new Size(130, 42);
            cancelButton.Click += cancelButton_Click;
            settingsActionPanel.Controls.Add(browseOutputButton);
            settingsActionPanel.Controls.Add(openOutputFolderButton);
            settingsActionPanel.Controls.Add(convertButton);
            settingsActionPanel.Controls.Add(cancelButton);
            settingsPanel.Controls.Add(settingsActionPanel);
            settingsPanel.Controls.Add(outputLayoutPanel);
            settingsPanel.Controls.Add(settingsHeaderLabel);

            progressPanel.BackColor = Color.FromArgb(252, 254, 255);
            progressPanel.CornerRadius = 22;
            progressPanel.Dock = DockStyle.Fill;
            progressPanel.Margin = new Padding(0, 14, 0, 0);
            progressPanel.Padding = new Padding(18, 12, 18, 12);
            progressTitleLabel.Text = "📈 حالة التقدم";
            progressTitleLabel.Dock = DockStyle.Top;
            progressTitleLabel.Height = 28;
            progressTitleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            progressTitleLabel.ForeColor = Color.FromArgb(12, 74, 110);
            progressTitleLabel.TextAlign = ContentAlignment.MiddleRight;
            progressBar.Dock = DockStyle.Top;
            progressBar.Height = 22;
            progressBar.Value = 0;
            progressBar.Margin = new Padding(0, 12, 0, 0);
            progressPanel.Controls.Add(progressBar);
            progressPanel.Controls.Add(progressTitleLabel);

            logPanel.BackColor = Color.White;
            logPanel.CornerRadius = 22;
            logPanel.Dock = DockStyle.Fill;
            logPanel.Margin = new Padding(0, 14, 0, 0);
            logPanel.Padding = new Padding(16, 10, 16, 14);
            toggleLogButton.Text = "📜 إخفاء السجل";
            toggleLogButton.Dock = DockStyle.Top;
            toggleLogButton.Height = 36;
            toggleLogButton.FillColor = Color.FromArgb(224, 247, 250);
            toggleLogButton.HoverColor = ColorTranslator.FromHtml("#8EF7FF");
            toggleLogButton.Click += toggleLogButton_Click;
            logRichTextBox.Dock = DockStyle.Fill;
            logRichTextBox.ReadOnly = true;
            logRichTextBox.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            logRichTextBox.BorderStyle = BorderStyle.None;
            logRichTextBox.BackColor = Color.FromArgb(247, 253, 255);
            logRichTextBox.ForeColor = Color.FromArgb(12, 74, 110);
            logRichTextBox.Margin = new Padding(0, 10, 0, 0);
            logPanel.Controls.Add(logRichTextBox);
            logPanel.Controls.Add(toggleLogButton);

            contentLayoutPanel.Controls.Add(filesPanel, 0, 0);
            contentLayoutPanel.Controls.Add(settingsPanel, 1, 0);
            rootLayoutPanel.Controls.Add(headerPanel, 0, 0);
            rootLayoutPanel.Controls.Add(contentLayoutPanel, 0, 1);
            rootLayoutPanel.Controls.Add(progressPanel, 0, 2);
            rootLayoutPanel.Controls.Add(logPanel, 0, 3);

            statusStrip.BackColor = Color.FromArgb(236, 252, 255);
            statusStrip.Dock = DockStyle.Bottom;
            statusStrip.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            statusStrip.RightToLeft = RightToLeft.Yes;
            statusStrip.Items.Add(statusLabel);
            statusStrip.Items.Add(new ToolStripSeparator());
            statusStrip.Items.Add(totalFilesStatusLabel);
            statusStrip.Items.Add(new ToolStripSeparator());
            statusStrip.Items.Add(readyFilesStatusLabel);
            statusLabel.Text = "جاهز";
            statusLabel.Spring = true;
            totalFilesStatusLabel.Text = "Total Files: 0";
            readyFilesStatusLabel.Text = "Ready Files: 0";

            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(250, 252, 255);
            ClientSize = new Size(1180, 780);
            Controls.Add(rootLayoutPanel);
            Controls.Add(statusStrip);
            Controls.Add(mainToolStrip);
            Controls.Add(mainMenuStrip);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.Sizable;
            MainMenuStrip = mainMenuStrip;
            MinimumSize = new Size(980, 700);
            Name = "MainForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GIS Universal Converter Pro";
            AllowDrop = true;
            DragEnter += MainForm_DragEnter;
            DragDrop += MainForm_DragDrop;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
