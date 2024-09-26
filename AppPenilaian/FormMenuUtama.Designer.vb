<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMenuUtama
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMenuUtama))
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoginToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogoutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KeluarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MasterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdminToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KaryawanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KriteriaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NilaiBobotToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataAlternatifToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CripsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PerhitunganToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HitungToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip2
        '
        Me.MenuStrip2.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.MasterToolStripMenuItem, Me.DataAlternatifToolStripMenuItem, Me.CripsToolStripMenuItem, Me.PerhitunganToolStripMenuItem})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Size = New System.Drawing.Size(1319, 33)
        Me.MenuStrip2.TabIndex = 0
        Me.MenuStrip2.Text = "MenuStrip2"
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Image = Global.AppPenilaian.My.Resources.Resources.gambar_green_laundry
        Me.PictureBox1.Location = New System.Drawing.Point(0, 33)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(1319, 626)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoginToolStripMenuItem, Me.LogoutToolStripMenuItem, Me.KeluarToolStripMenuItem})
        Me.FileToolStripMenuItem.Image = Global.AppPenilaian.My.Resources.Resources.Menu_icon_2_icon_icons_com_71856
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(78, 29)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'LoginToolStripMenuItem
        '
        Me.LoginToolStripMenuItem.Image = Global.AppPenilaian.My.Resources.Resources.login_arrow_symbol_entering_back_into_a_square_icon_icons_com_73221
        Me.LoginToolStripMenuItem.Name = "LoginToolStripMenuItem"
        Me.LoginToolStripMenuItem.Size = New System.Drawing.Size(171, 34)
        Me.LoginToolStripMenuItem.Text = "Login"
        '
        'LogoutToolStripMenuItem
        '
        Me.LogoutToolStripMenuItem.Image = Global.AppPenilaian.My.Resources.Resources.logout_circle_icon_206010
        Me.LogoutToolStripMenuItem.Name = "LogoutToolStripMenuItem"
        Me.LogoutToolStripMenuItem.Size = New System.Drawing.Size(171, 34)
        Me.LogoutToolStripMenuItem.Text = "Logout"
        '
        'KeluarToolStripMenuItem
        '
        Me.KeluarToolStripMenuItem.Image = Global.AppPenilaian.My.Resources.Resources._4115235_exit_logout_sign_out_114030
        Me.KeluarToolStripMenuItem.Name = "KeluarToolStripMenuItem"
        Me.KeluarToolStripMenuItem.Size = New System.Drawing.Size(171, 34)
        Me.KeluarToolStripMenuItem.Text = "Keluar"
        '
        'MasterToolStripMenuItem
        '
        Me.MasterToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AdminToolStripMenuItem, Me.KaryawanToolStripMenuItem, Me.KriteriaToolStripMenuItem, Me.NilaiBobotToolStripMenuItem, Me.AToolStripMenuItem})
        Me.MasterToolStripMenuItem.Image = Global.AppPenilaian.My.Resources.Resources.server_rack_telecommunications_data_server_network_devices_data_center_icon_193885
        Me.MasterToolStripMenuItem.Name = "MasterToolStripMenuItem"
        Me.MasterToolStripMenuItem.Size = New System.Drawing.Size(148, 29)
        Me.MasterToolStripMenuItem.Text = "Master Data"
        '
        'AdminToolStripMenuItem
        '
        Me.AdminToolStripMenuItem.Image = Global.AppPenilaian.My.Resources.Resources.admin_user_icon_188317
        Me.AdminToolStripMenuItem.Name = "AdminToolStripMenuItem"
        Me.AdminToolStripMenuItem.Size = New System.Drawing.Size(202, 34)
        Me.AdminToolStripMenuItem.Text = "Admin"
        '
        'KaryawanToolStripMenuItem
        '
        Me.KaryawanToolStripMenuItem.Image = Global.AppPenilaian.My.Resources.Resources.effective_employees_users_team_group_icon_152042
        Me.KaryawanToolStripMenuItem.Name = "KaryawanToolStripMenuItem"
        Me.KaryawanToolStripMenuItem.Size = New System.Drawing.Size(202, 34)
        Me.KaryawanToolStripMenuItem.Text = "Karyawan"
        '
        'KriteriaToolStripMenuItem
        '
        Me.KriteriaToolStripMenuItem.Image = Global.AppPenilaian.My.Resources.Resources.checklist_38554
        Me.KriteriaToolStripMenuItem.Name = "KriteriaToolStripMenuItem"
        Me.KriteriaToolStripMenuItem.Size = New System.Drawing.Size(202, 34)
        Me.KriteriaToolStripMenuItem.Text = "Kriteria"
        '
        'NilaiBobotToolStripMenuItem
        '
        Me.NilaiBobotToolStripMenuItem.Image = Global.AppPenilaian.My.Resources.Resources.exposure_value_48_46071
        Me.NilaiBobotToolStripMenuItem.Name = "NilaiBobotToolStripMenuItem"
        Me.NilaiBobotToolStripMenuItem.Size = New System.Drawing.Size(202, 34)
        Me.NilaiBobotToolStripMenuItem.Text = "Nilai Bobot"
        '
        'AToolStripMenuItem
        '
        Me.AToolStripMenuItem.Image = Global.AppPenilaian.My.Resources.Resources.sort_by_attributes_icon_icons_com_73406
        Me.AToolStripMenuItem.Name = "AToolStripMenuItem"
        Me.AToolStripMenuItem.Size = New System.Drawing.Size(202, 34)
        Me.AToolStripMenuItem.Text = "Atribut"
        '
        'DataAlternatifToolStripMenuItem
        '
        Me.DataAlternatifToolStripMenuItem.Image = Global.AppPenilaian.My.Resources.Resources.analysis_visualization_data_graph_chart_icon_262765
        Me.DataAlternatifToolStripMenuItem.Name = "DataAlternatifToolStripMenuItem"
        Me.DataAlternatifToolStripMenuItem.Size = New System.Drawing.Size(166, 29)
        Me.DataAlternatifToolStripMenuItem.Text = "Data Alternatif"
        '
        'CripsToolStripMenuItem
        '
        Me.CripsToolStripMenuItem.Image = Global.AppPenilaian.My.Resources.Resources.index_data_icon_161123
        Me.CripsToolStripMenuItem.Name = "CripsToolStripMenuItem"
        Me.CripsToolStripMenuItem.Size = New System.Drawing.Size(92, 29)
        Me.CripsToolStripMenuItem.Text = "Crips"
        '
        'PerhitunganToolStripMenuItem
        '
        Me.PerhitunganToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HitungToolStripMenuItem})
        Me.PerhitunganToolStripMenuItem.Image = Global.AppPenilaian.My.Resources.Resources.calculation_count_finance_calculator_calc_math_icon_256453
        Me.PerhitunganToolStripMenuItem.Name = "PerhitunganToolStripMenuItem"
        Me.PerhitunganToolStripMenuItem.Size = New System.Drawing.Size(146, 29)
        Me.PerhitunganToolStripMenuItem.Text = "Perhitungan"
        '
        'HitungToolStripMenuItem
        '
        Me.HitungToolStripMenuItem.Image = Global.AppPenilaian.My.Resources.Resources._4213589_calculate_calculator_doodle_education_line_school_vector_115495
        Me.HitungToolStripMenuItem.Name = "HitungToolStripMenuItem"
        Me.HitungToolStripMenuItem.Size = New System.Drawing.Size(168, 34)
        Me.HitungToolStripMenuItem.Text = "Hitung"
        '
        'FormMenuUtama
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1319, 659)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormMenuUtama"
        Me.Text = "Menu Utama Aplikasi Penilaian"
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoginToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LogoutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents KeluarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MasterToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AdminToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents KaryawanToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CripsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PerhitunganToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents KriteriaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NilaiBobotToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HitungToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DataAlternatifToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PictureBox1 As PictureBox
End Class
