Imports MySql.Data.MySqlClient

Public Class Hasil_Rangking
    Private Sub Hasil_Rangking_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TampilkanPeringkat()
    End Sub
    Sub tampilhasilrangking()
        Call koneksi()
        da = New MySqlDataAdapter("select * from hasil_saw", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "hasil_saw")
        dgv.DataSource = ds.Tables("hasil_saw")
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv.RowsDefaultCellStyle.BackColor = Color.LightBlue
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgv.ReadOnly = True
        tutupdb()

    End Sub
    Private Sub TampilkanPeringkat()
        Try
            Call koneksi()
            Dim query As String = "SELECT Karyawan_Id, total_score FROM hasil_saw ORDER BY total_score DESC"
            da = New MySqlDataAdapter(query, conn)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds, "hasil_saw")

            ' Tambahkan kolom peringkat
            ds.Tables("hasil_saw").Columns.Add("Peringkat", GetType(Integer))

            ' Isi kolom peringkat
            For i As Integer = 0 To ds.Tables("hasil_saw").Rows.Count - 1
                ds.Tables("hasil_saw").Rows(i)("Peringkat") = i + 1
            Next

            ' Bind data ke DataGridView
            dgv.DataSource = ds.Tables("hasil_saw")
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            dgv.RowsDefaultCellStyle.BackColor = Color.LightBlue
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgv.ReadOnly = True

            tutupdb()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub


End Class
