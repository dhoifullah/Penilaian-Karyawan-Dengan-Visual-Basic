Imports System.Data.Odbc
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Public Class FormMenuUtama




    Sub Terkunci()
        LoginToolStripMenuItem.Visible = True
        LogoutToolStripMenuItem.Visible = False
        MasterToolStripMenuItem.Visible = False
        AdminToolStripMenuItem.Visible = False
        DataAlternatifToolStripMenuItem.Visible = False
        CripsToolStripMenuItem.Visible = False
        PerhitunganToolStripMenuItem.Visible = False

    End Sub




    Private Sub FormMenuUtama_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Terkunci()
        Call koneksi()
        Me.WindowState = FormWindowState.Maximized ' Set formulir ke mode layar penuh

    End Sub

    Private Sub KeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KeluarToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub LoginToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoginToolStripMenuItem.Click
        Dim newForm As New FormLogin() ' Gantilah "Form1" dengan nama formulir Anda
        newForm.StartPosition = FormStartPosition.CenterScreen ' Menampilkan jendela di tengah layar
        FormLogin.ShowDialog()

    End Sub

    Private Sub FileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileToolStripMenuItem.Click

    End Sub

    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click

        For Each frm As Form In Application.OpenForms
            If frm IsNot Me Then
                frm.Close()
                frm.Dispose()
            End If
        Next frm


        Dim keluar As String
        keluar = MsgBox("Yakin Anda ingin keluar..?", vbQuestion + vbYesNo, "Konfirmasi")
        If keluar = vbYes Then
            Call Terkunci()





        ElseIf keluar = vbNo Then

        End If

    End Sub


    Private Sub AdminToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdminToolStripMenuItem.Click
        Call koneksi()
        Dim newForm As New Admin() ' Gantilah "Form1" dengan nama formulir Anda
        newForm.StartPosition = FormStartPosition.CenterScreen ' Menampilkan jendela di tengah layar
        Admin.ShowDialog()
    End Sub

    Private Sub KaryawanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KaryawanToolStripMenuItem.Click
        Dim newForm As New Karyawan() ' Gantilah "Form1" dengan nama formulir Anda
        newForm.StartPosition = FormStartPosition.CenterScreen ' Menampilkan jendela di tengah layar
        Karyawan.ShowDialog()
    End Sub











    Private Sub KriteriaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KriteriaToolStripMenuItem.Click
        Dim newForm As New Kriteria() ' Gantilah "Form1" dengan nama formulir Anda
        newForm.StartPosition = FormStartPosition.CenterScreen ' Menampilkan jendela di tengah layar
        Kriteria.ShowDialog()
    End Sub






    Private Sub NilaiBobotToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NilaiBobotToolStripMenuItem.Click
        Dim newForm As New Bobot() ' Gantilah "Form1" dengan nama formulir Anda
        newForm.StartPosition = FormStartPosition.CenterScreen ' Menampilkan jendela di tengah layar
        Bobot.ShowDialog()
    End Sub

    Private Sub HitungToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HitungToolStripMenuItem.Click
        Hitung.ShowDialog()

    End Sub

    Private Sub HasilRangkingToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Hasil_Rangking.Show()
    End Sub

    Private Sub CripsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CripsToolStripMenuItem.Click
        Dim newForm As New crips() ' Gantilah "Form1" dengan nama formulir Anda
        newForm.StartPosition = FormStartPosition.CenterScreen ' Menampilkan jendela di tengah layar
        crips.Show()
    End Sub

    Private Sub AToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AToolStripMenuItem.Click
        Dim newForm As New Atribut() ' Gantilah "Form1" dengan nama formulir Anda
        newForm.StartPosition = FormStartPosition.CenterScreen ' Menampilkan jendela di tengah layar
        Atribut.Show()

    End Sub

    Private Sub DataAlternatifToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataAlternatifToolStripMenuItem.Click
        Data_Alternatif.Show()
    End Sub
End Class
