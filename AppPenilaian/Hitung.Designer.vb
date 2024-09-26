<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Hitung
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Hitung))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.MatrikTernormalisasiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.AnalisaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HasilAnalisaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NormalisasiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.dgv)
        Me.Panel1.Controls.Add(Me.MenuStrip1)
        Me.Panel1.Location = New System.Drawing.Point(34, 77)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(974, 342)
        Me.Panel1.TabIndex = 3
        '
        'dgv
        '
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Location = New System.Drawing.Point(26, 46)
        Me.dgv.Name = "dgv"
        Me.dgv.RowHeadersWidth = 62
        Me.dgv.RowTemplate.Height = 28
        Me.dgv.Size = New System.Drawing.Size(921, 274)
        Me.dgv.TabIndex = 1
        '
        'MenuStrip1
        '
        Me.MenuStrip1.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AnalisaToolStripMenuItem, Me.HasilAnalisaToolStripMenuItem, Me.NormalisasiToolStripMenuItem, Me.MatrikTernormalisasiToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(972, 36)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'MatrikTernormalisasiToolStripMenuItem
        '
        Me.MatrikTernormalisasiToolStripMenuItem.Image = Global.AppPenilaian.My.Resources.Resources.matrix_icon_137416
        Me.MatrikTernormalisasiToolStripMenuItem.Name = "MatrikTernormalisasiToolStripMenuItem"
        Me.MatrikTernormalisasiToolStripMenuItem.Size = New System.Drawing.Size(218, 30)
        Me.MatrikTernormalisasiToolStripMenuItem.Text = "Matrik Ternormalisasi"
        '
        'PrintDocument1
        '
        '
        'AnalisaToolStripMenuItem
        '
        Me.AnalisaToolStripMenuItem.Image = Global.AppPenilaian.My.Resources.Resources.preliminary_examination_disease_icon_145854
        Me.AnalisaToolStripMenuItem.Name = "AnalisaToolStripMenuItem"
        Me.AnalisaToolStripMenuItem.Size = New System.Drawing.Size(90, 30)
        Me.AnalisaToolStripMenuItem.Text = "Awal"
        '
        'HasilAnalisaToolStripMenuItem
        '
        Me.HasilAnalisaToolStripMenuItem.Image = Global.AppPenilaian.My.Resources.Resources.graph_analytics_charts_data_study_anaysis_icon_143871
        Me.HasilAnalisaToolStripMenuItem.Name = "HasilAnalisaToolStripMenuItem"
        Me.HasilAnalisaToolStripMenuItem.Size = New System.Drawing.Size(151, 30)
        Me.HasilAnalisaToolStripMenuItem.Text = "Hasil Analisa"
        '
        'NormalisasiToolStripMenuItem
        '
        Me.NormalisasiToolStripMenuItem.Image = Global.AppPenilaian.My.Resources.Resources.fileinterfacesymboloftextpapersheet_79740
        Me.NormalisasiToolStripMenuItem.Name = "NormalisasiToolStripMenuItem"
        Me.NormalisasiToolStripMenuItem.Size = New System.Drawing.Size(144, 30)
        Me.NormalisasiToolStripMenuItem.Text = "Normalisasi"
        '
        'Button3
        '
        Me.Button3.Image = Global.AppPenilaian.My.Resources.Resources.back_button_circular_left_arrow_symbol_icon_icons_com_74326
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button3.Location = New System.Drawing.Point(271, 21)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(124, 54)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "Keluar"
        Me.Button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Image = Global.AppPenilaian.My.Resources.Resources._2849806_copy_interface_multimedia_print_printer_107972
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.Location = New System.Drawing.Point(145, 21)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(120, 54)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Cetak"
        Me.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Image = Global.AppPenilaian.My.Resources.Resources._4213589_calculate_calculator_doodle_education_line_school_vector_115495
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(34, 21)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(105, 54)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Hitung"
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Hitung
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.ClientSize = New System.Drawing.Size(1052, 450)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Hitung"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Hitung"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents AnalisaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HasilAnalisaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NormalisasiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents dgv As DataGridView
    Friend WithEvents MatrikTernormalisasiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
End Class
