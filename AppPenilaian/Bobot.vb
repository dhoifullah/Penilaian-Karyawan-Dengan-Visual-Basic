Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class Bobot
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' Membersihkan semua bidang input
        ComboBox1.SelectedItem = Nothing
        TextBox2.Text = ""
        TextBox1.Text = ""
    End Sub
    Sub kosong()

        TextBox1.Clear()
        TextBox2.Clear()

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            Dim query As String = "SELECT * FROM bobot WHERE id_bobot = @id_bobot"
            cmd = New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@id_bobot", TextBox1.Text)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                TextBox2.Text = dr("kriteria_bobot").ToString()

            Else
                MsgBox("Kode Tidak Ditemukan")
            End If

            tutupdb()
        End If
    End Sub

    Sub tampil()
        Try
            Call koneksi()
            Dim query As String = "SELECT bobot.*, kriteria.nama_kriteria FROM bobot INNER JOIN kriteria ON bobot.kode_kriteria = kriteria.kode_kriteria"
            da = New MySqlDataAdapter(query, conn)
            ds = New DataSet()
            da.Fill(ds, "bobot_kriteria")
            dgv.DataSource = ds.Tables("bobot_kriteria")
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            dgv.RowsDefaultCellStyle.BackColor = Color.LightBlue
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgv.ReadOnly = True
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        Finally
            tutupdb()
        End Try
    End Sub






    Private Sub Bobot_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampil()
        Munculkodekriteria()
        TextBox1.ReadOnly = True
    End Sub
    Sub Munculkodekriteria()
        Call koneksi()
        ComboBox1.Items.Clear()
        ComboBox1.Items.Clear()
        Dim cmd As MySqlCommand = New MySqlCommand("SELECT * FROM kriteria", conn)
        Dim dr As MySqlDataReader = cmd.ExecuteReader
        While dr.Read()
            ComboBox1.Items.Add(dr.Item("kode_kriteria")) ' Ganti "nama_field_yang_diinginkan" dengan nama kolom yang ingin ditampilkan di ComboBox
        End While
        dr.Close()
        conn.Close()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        Try
            Call koneksi()

            ' Pengecekan apakah ComboBox memiliki item
            If ComboBox1.Items.Count = 0 Then
                MessageBox.Show("Tidak ada data yang tersedia di ComboBox.")
                Exit Sub
            End If

            ' Pengecekan apakah item telah dipilih di ComboBox
            If ComboBox1.SelectedItem Is Nothing Then
                MessageBox.Show("Harap pilih kode kriteria sebelum menyimpan.")
                Exit Sub
            End If

            Dim kode_kriteria As String = ComboBox1.SelectedItem.ToString()
            Dim nilai_bobot As String = TextBox2.Text

            ' Pengecekan apakah nilai_bobot adalah numerik
            Dim isNumericValue As Boolean = IsNumeric(nilai_bobot)

            If nilai_bobot = "" Then
                MessageBox.Show("Harap lengkapi semua kolom sebelum menyimpan.")
                Exit Sub
            ElseIf Not isNumericValue Then
                MessageBox.Show("Nilai bobot harus berupa angka.")
                Exit Sub
            End If

            ' Pengecekan apakah kode kriteria sudah ada di dalam database pada tabel bobot
            Dim cek_kriteria_query As String = "SELECT COUNT(*) FROM bobot WHERE kode_kriteria = @kode_kriteria"
            Dim cmd_cek_kriteria As New MySqlCommand(cek_kriteria_query, conn)
            cmd_cek_kriteria.Parameters.AddWithValue("@kode_kriteria", kode_kriteria)
            Dim kriteria_count As Integer = Convert.ToInt32(cmd_cek_kriteria.ExecuteScalar())

            If kriteria_count > 0 Then
                MessageBox.Show("Kode kriteria sudah ada dalam database.")
                Exit Sub
            End If

            Dim simpan As String = "INSERT INTO bobot (kode_kriteria, nilai_bobot) VALUES (@kode, @nilai)"
            Dim cmd As New MySqlCommand(simpan, conn)
            cmd.Parameters.AddWithValue("@kode", kode_kriteria)
            cmd.Parameters.AddWithValue("@nilai", nilai_bobot)

            cmd.ExecuteNonQuery()

            MsgBox("Data Berhasil Disimpan")
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        Finally
            tampil()
            kosong()
            conn.Close()
            tutupdb()
        End Try

    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Call koneksi()

            If ComboBox1.SelectedItem Is Nothing OrElse String.IsNullOrWhiteSpace(TextBox2.Text) Then
                MessageBox.Show("Harap lengkapi semua kolom sebelum mengupdate.")
                Exit Sub
            End If

            Dim kode_kriteria As String = ComboBox1.SelectedItem.ToString()
            Dim nilai_bobot As String = TextBox2.Text

            ' Pengecekan apakah nilai_bobot adalah numerik
            Dim isNumericValue As Boolean = IsNumeric(nilai_bobot)

            If Not isNumericValue Then
                MessageBox.Show("Nilai bobot harus berupa angka.")
                Exit Sub
            End If

            ' Pengecekan apakah kode kriteria sudah ada dalam database pada tabel bobot
            Dim cek_kriteria_query As String = "SELECT COUNT(*) FROM bobot WHERE kode_kriteria = @kode_kriteria"
            Dim cmd_cek_kriteria As New MySqlCommand(cek_kriteria_query, conn)
            cmd_cek_kriteria.Parameters.AddWithValue("@kode_kriteria", kode_kriteria)
            Dim kriteria_count As Integer = Convert.ToInt32(cmd_cek_kriteria.ExecuteScalar())

            If kriteria_count = 0 Then
                MessageBox.Show("Kode kriteria tidak ditemukan dalam database.")
                Exit Sub
            End If

            Dim update As String = "UPDATE bobot SET nilai_bobot = @nilai WHERE kode_kriteria = @kode"
            Dim cmd As New MySqlCommand(update, conn)
            cmd.Parameters.AddWithValue("@nilai", nilai_bobot)
            cmd.Parameters.AddWithValue("@kode", kode_kriteria)

            cmd.ExecuteNonQuery()

            MsgBox("Data Berhasil Diupdate")
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        Finally
            tampil()
            kosong()
            conn.Close()
            tutupdb()
        End Try

    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        If ComboBox1.Text = "" Then
            MsgBox("Kode harus diisi!")
        Else
            If MsgBox("Yakin data mau dihapus?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Try
                    Call koneksi()

                    ' Pengecekan apakah kode kriteria ada dalam database
                    Dim cek_kriteria_query As String = "SELECT COUNT(*) FROM bobot WHERE kode_kriteria = @kode_kriteria"
                    Dim cmd_cek_kriteria As New MySqlCommand(cek_kriteria_query, conn)
                    cmd_cek_kriteria.Parameters.AddWithValue("@kode_kriteria", ComboBox1.Text)
                    Dim kriteria_count As Integer = Convert.ToInt32(cmd_cek_kriteria.ExecuteScalar())

                    If kriteria_count = 0 Then
                        MessageBox.Show("Data tidak ditemukan dalam database.")
                        Exit Sub
                    End If

                    Dim hapus As String = "DELETE FROM bobot WHERE kode_kriteria = @kode_kriteria"
                    cmd = New MySqlCommand(hapus, conn)
                    cmd.Parameters.AddWithValue("@kode_kriteria", ComboBox1.Text)
                    cmd.ExecuteNonQuery()

                    MsgBox("Data berhasil dihapus.")
                    kosong()
                    tampil()
                Catch ex As Exception
                    MessageBox.Show("Terjadi kesalahan: " & ex.Message)
                Finally
                    tutupdb()
                End Try
            End If
        End If


    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub
End Class
