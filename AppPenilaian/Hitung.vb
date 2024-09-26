Imports System.IO
Imports System.Windows.Forms.VisualStyles
Imports MySql.Data.MySqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Text

Public Class Hitung

    Private Sub Hitung_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Sub ClearDataGridView()
        dgv.DataSource = Nothing
        dgv.Rows.Clear()
        dgv.Columns.Clear()
    End Sub

    Sub tampil()
        Try
            Call koneksi()

            ' Bersihkan data pada DataGridView
            dgv.Columns.Clear()

            ' Buat kueri SQL untuk mengambil data dari tabel data_alternatif
            Dim query As String = "SELECT * FROM data_alternatif"
            Dim adapter As New MySqlDataAdapter(query, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ' Set data hasil kueri ke DataGridView
            dgv.DataSource = dt
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            dgv.RowsDefaultCellStyle.BackColor = Color.LightBlue
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgv.ReadOnly = True

            ' Tutup koneksi
            tutupdb()

        Catch ex As Exception
            ' Tangani kesalahan di sini
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TampilAnalisaData()
        Try
            Call koneksi()

            ' Bersihkan data pada DataGridView
            dgv.Rows.Clear()

            ' Tambahkan kolom ke DataGridView
            dgv.Columns.Clear() ' Hapus semua kolom sebelum menambahkan kolom baru
            dgv.Columns.Add("Alternatif", "Alternatif")
            dgv.Columns.Add("Kehadiran", "Kehadiran")
            dgv.Columns.Add("Kedisiplinan", "Kedisiplinan")
            dgv.Columns.Add("Kinerja", "Kinerja")
            dgv.Columns.Add("Hukuman", "Hukuman")
            dgv.Columns.Add("Keterampilan", "Keterampilan")

            ' Buat kueri SQL untuk mengambil data analisa dari tabel data_alternatif dan crips
            Dim queryAnalisa As String = "SELECT da.alternatif, " &
                                  "(SELECT bobot_crips FROM crips WHERE kode = da.kehadiran) AS kehadiran, " &
                                  "(SELECT bobot_crips FROM crips WHERE kode = da.kedisiplinan) AS kedisiplinan, " &
                                  "(SELECT bobot_crips FROM crips WHERE kode = da.kinerja) AS kinerja, " &
                                  "(SELECT bobot_crips FROM crips WHERE kode = da.hukuman) AS hukuman, " &
                                  "(SELECT bobot_crips FROM crips WHERE kode = da.keterampilan) AS keterampilan " &
                                  "FROM data_alternatif da"

            Dim adapterAnalisa As New MySqlDataAdapter(queryAnalisa, conn)
            Dim dtAnalisa As New DataTable()
            adapterAnalisa.Fill(dtAnalisa)

            ' Tambahkan data analisa ke DataGridView
            For Each row As DataRow In dtAnalisa.Rows
                dgv.Rows.Add(row("alternatif"), row("kehadiran"), row("kedisiplinan"), row("kinerja"), row("hukuman"), row("keterampilan"))
            Next

            ' Tutup koneksi
            tutupdb()

        Catch ex As Exception
            ' Tangani kesalahan di sini
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub





    Private Sub AnalisaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AnalisaToolStripMenuItem.Click
        ClearDataGridView()
        tampil()

    End Sub

    Private Sub HasilAnalisaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HasilAnalisaToolStripMenuItem.Click
        ClearDataGridView()
        TampilAnalisaData()
    End Sub

    Private hasilNormalisasi As DataTable ' Deklarasikan hasilNormalisasi di luar dari sub-routine agar dapat diakses di sub-routine lainnya

    Private Sub NormalisasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NormalisasiToolStripMenuItem.Click
        Try
            ' Memanggil fungsi koneksi
            Call koneksi()

            ' Panggil fungsi TampilAnalisaData untuk mengambil data
            TampilAnalisaData()

            ' Mendapatkan nilai maksimum untuk setiap kolom
            Dim maxKehadiran As Double = dgv.Rows.Cast(Of DataGridViewRow)().Max(Function(row) If(row.Cells("Kehadiran").Value IsNot DBNull.Value, Convert.ToDouble(row.Cells("Kehadiran").Value), Double.MinValue))
            Dim maxKedisiplinan As Double = dgv.Rows.Cast(Of DataGridViewRow)().Max(Function(row) If(row.Cells("Kedisiplinan").Value IsNot DBNull.Value, Convert.ToDouble(row.Cells("Kedisiplinan").Value), Double.MinValue))
            Dim maxKinerja As Double = dgv.Rows.Cast(Of DataGridViewRow)().Max(Function(row) If(row.Cells("Kinerja").Value IsNot DBNull.Value, Convert.ToDouble(row.Cells("Kinerja").Value), Double.MinValue))
            Dim minHukuman As Double = dgv.Rows.Cast(Of DataGridViewRow)().Min(Function(row) If(row.Cells("Hukuman").Value IsNot DBNull.Value, Convert.ToDouble(row.Cells("Hukuman").Value), Double.MaxValue))
            Dim maxKeterampilan As Double = dgv.Rows.Cast(Of DataGridViewRow)().Max(Function(row) If(row.Cells("Keterampilan").Value IsNot DBNull.Value, Convert.ToDouble(row.Cells("Keterampilan").Value), Double.MinValue))

            ' Jika nilai terkecil Hukuman adalah nol, cari nilai terkecil yang bukan nol
            If minHukuman = 0 Then
                minHukuman = dgv.Rows.Cast(Of DataGridViewRow)().Where(Function(row) Convert.ToDouble(row.Cells("Hukuman").Value) <> 0).Min(Function(row) Convert.ToDouble(row.Cells("Hukuman").Value))
            End If

            ' Buat DataTable untuk menyimpan hasil normalisasi
            hasilNormalisasi = New DataTable()
            hasilNormalisasi.Columns.Add("Alternatif", GetType(String))
            hasilNormalisasi.Columns.Add("Kehadiran", GetType(Double))
            hasilNormalisasi.Columns.Add("Kedisiplinan", GetType(Double))
            hasilNormalisasi.Columns.Add("Kinerja", GetType(Double))
            hasilNormalisasi.Columns.Add("Hukuman", GetType(Double))
            hasilNormalisasi.Columns.Add("Keterampilan", GetType(Double))

            ' Bagi setiap nilai dalam setiap kolom dengan nilai maksimum yang sesuai
            For Each row As DataGridViewRow In dgv.Rows
                Dim newRow As DataRow = hasilNormalisasi.NewRow()

                newRow("Alternatif") = If(row.Cells("Alternatif").Value IsNot DBNull.Value, Convert.ToString(row.Cells("Alternatif").Value), Nothing)

                ' Pemeriksaan kolom yang kosong sebelum menambahkan baris ke hasilNormalisasi
                Dim isRowEmpty As Boolean = True
                For Each cell As DataGridViewCell In row.Cells
                    If cell.OwningColumn.Name <> "Alternatif" AndAlso cell.Value IsNot Nothing AndAlso Not cell.Value.Equals(DBNull.Value) Then
                        isRowEmpty = False
                        Exit For
                    End If
                Next

                ' Jika baris tidak kosong, tambahkan baris ke hasilNormalisasi
                If Not isRowEmpty Then
                    newRow("Kehadiran") = If(maxKehadiran <> 0, If(row.Cells("Kehadiran").Value IsNot DBNull.Value, Convert.ToDouble(row.Cells("Kehadiran").Value) / maxKehadiran, 0), 0)
                    newRow("Kedisiplinan") = If(maxKedisiplinan <> 0, If(row.Cells("Kedisiplinan").Value IsNot DBNull.Value, Convert.ToDouble(row.Cells("Kedisiplinan").Value) / maxKedisiplinan, 0), 0)
                    newRow("Kinerja") = If(maxKinerja <> 0, If(row.Cells("Kinerja").Value IsNot DBNull.Value, Convert.ToDouble(row.Cells("Kinerja").Value) / maxKinerja, 0), 0)
                    newRow("Hukuman") = If(minHukuman <> 0, If(row.Cells("Hukuman").Value IsNot DBNull.Value, Convert.ToDouble(row.Cells("Hukuman").Value) / minHukuman, 0), 0)
                    newRow("Keterampilan") = If(maxKeterampilan <> 0, If(row.Cells("Keterampilan").Value IsNot DBNull.Value, Convert.ToDouble(row.Cells("Keterampilan").Value) / maxKeterampilan, 0), 0)

                    hasilNormalisasi.Rows.Add(newRow)
                End If
            Next

            ' Tutup koneksi
            tutupdb()

            ' Tampilkan hasil normalisasi dalam form baru atau dialog
            Dim formHasilNormalisasi As New Form()
            Dim dgvHasilNormalisasi As New DataGridView()

            ' Atur properti AutoSizeColumnsMode untuk mengatur lebar kolom secara otomatis
            dgvHasilNormalisasi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            ' Atur DataSource dgvHasilNormalisasi ke hasilNormalisasi
            dgvHasilNormalisasi.DataSource = hasilNormalisasi

            ' Tambahkan DataGridView ke form
            formHasilNormalisasi.Controls.Add(dgvHasilNormalisasi)

            ' Tampilkan form sebagai dialog
            formHasilNormalisasi.ShowDialog()

        Catch ex As Exception
            ' Tangani kesalahan di sini
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MatrikTernormalisasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MatrikTernormalisasiToolStripMenuItem.Click
        Try
            ' Memanggil fungsi koneksi
            Call koneksi()

            ' Membersihkan kolom yang mungkin telah ditambahkan sebelumnya secara tidak sengaja
            dgv.Columns.Clear()

            ' Tambahkan kolom ke DataGridView
            dgv.Columns.Add("Alternatif", "Alternatif")
            dgv.Columns.Add("Kehadiran", "Kehadiran")
            dgv.Columns.Add("Kedisiplinan", "Kedisiplinan")
            dgv.Columns.Add("Kinerja", "Kinerja")
            dgv.Columns.Add("Hukuman", "Hukuman")
            dgv.Columns.Add("Keterampilan", "Keterampilan")

            ' Pastikan hasilNormalisasi tidak null
            If hasilNormalisasi IsNot Nothing AndAlso hasilNormalisasi.Rows.Count > 0 Then
                ' Tambahkan data hasil normalisasi ke DataGridView tanpa kolom yang kosong
                For Each row As DataRow In hasilNormalisasi.Rows
                    Dim isRowEmpty As Boolean = True ' Tentukan apakah baris kosong
                    ' Periksa setiap nilai dalam baris
                    For Each column As DataColumn In hasilNormalisasi.Columns
                        If row(column.ColumnName) IsNot DBNull.Value Then
                            isRowEmpty = False ' Jika ada nilai yang tidak null, baris tidak kosong
                            Exit For ' Keluar dari loop karena sudah ada nilai yang tidak null
                        End If
                    Next
                    ' Jika baris tidak kosong, tambahkan baris ke DataGridView
                    If Not isRowEmpty Then
                        Dim newRow As DataGridViewRow = dgv.Rows(dgv.Rows.Add()) ' Tambahkan baris baru
                        For Each column As DataColumn In hasilNormalisasi.Columns
                            If column.ColumnName = "Alternatif" OrElse column.ColumnName = "Kehadiran" OrElse column.ColumnName = "Kedisiplinan" OrElse column.ColumnName = "Kinerja" OrElse column.ColumnName = "Hukuman" OrElse column.ColumnName = "Keterampilan" Then
                                newRow.Cells(column.ColumnName).Value = If(row(column.ColumnName) IsNot DBNull.Value, row(column.ColumnName), Nothing) ' Isi nilai sel
                            End If
                        Next
                    End If
                Next

                ' Buat kueri SQL untuk mengambil nilai bobot dari tabel bobot
                Dim queryBobot As String = "SELECT nilai_bobot FROM bobot"
                Dim adapterBobot As New MySqlDataAdapter(queryBobot, conn)
                Dim dtBobot As New DataTable()
                adapterBobot.Fill(dtBobot)

                ' Mendapatkan nilai bobot dari tabel bobot
                Dim nilaiBobot As Double() = dtBobot.AsEnumerable().Select(Function(row) Convert.ToDouble(row("nilai_bobot"))).ToArray()

                ' Hitung hasil normalisasi dan perkalian dengan nilai bobot
                For Each row As DataGridViewRow In dgv.Rows
                    For Each cell As DataGridViewCell In row.Cells
                        If cell.OwningColumn.Name <> "Alternatif" Then
                            Dim nilai As Double
                            If cell.Value IsNot Nothing AndAlso Not cell.Value.Equals(DBNull.Value) Then
                                nilai = Convert.ToDouble(cell.Value)
                            Else
                                nilai = 0 ' Nilai default jika nilai null
                            End If

                            Dim kolomIndex As Integer = cell.ColumnIndex - 1 ' Indeks untuk mengakses nilai bobot
                            cell.Value = nilai * nilaiBobot(kolomIndex)
                        End If
                    Next
                Next
            End If

            ' Tutup koneksi
            tutupdb()

        Catch ex As Exception
            ' Tangani kesalahan di sini
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub




    Private Sub ProsesPeringkat()
        Try
            ' Membersihkan DataGridView
            dgv.Rows.Clear()
            dgv.Columns.Clear()

            ' Tambahkan kolom ke DataGridView
            dgv.Columns.Add("Ranking", "Ranking") ' Kolom peringkat
            dgv.Columns.Add("Alternatif", "Alternatif")
            dgv.Columns.Add("Kehadiran", "Kehadiran")
            dgv.Columns.Add("Kedisiplinan", "Kedisiplinan")
            dgv.Columns.Add("Kinerja", "Kinerja")
            dgv.Columns.Add("Hukuman", "Hukuman")
            dgv.Columns.Add("Keterampilan", "Keterampilan")
            dgv.Columns.Add("Total", "Total") ' Kolom untuk total nilai

            ' Pastikan hasilNormalisasi tidak null
            If hasilNormalisasi IsNot Nothing AndAlso hasilNormalisasi.Rows.Count > 0 Then
                ' Tambahkan data hasil normalisasi ke DataGridView tanpa kolom yang kosong
                For Each row As DataRow In hasilNormalisasi.Rows
                    Dim isRowEmpty As Boolean = True ' Tentukan apakah baris kosong
                    ' Periksa setiap nilai dalam baris
                    For Each column As DataColumn In hasilNormalisasi.Columns
                        If row(column.ColumnName) IsNot DBNull.Value Then
                            isRowEmpty = False ' Jika ada nilai yang tidak null, baris tidak kosong
                            Exit For ' Keluar dari loop karena sudah ada nilai yang tidak null
                        End If
                    Next
                    ' Jika baris tidak kosong, tambahkan baris ke DataGridView
                    If Not isRowEmpty Then
                        dgv.Rows.Add(
                            0, ' Kolom Ranking dimulai dengan nilai 0
                            If(row("Alternatif") IsNot DBNull.Value, row("Alternatif"), Nothing),
                            If(row("Kehadiran") IsNot DBNull.Value, row("Kehadiran"), Nothing),
                            If(row("Kedisiplinan") IsNot DBNull.Value, row("Kedisiplinan"), Nothing),
                            If(row("Kinerja") IsNot DBNull.Value, row("Kinerja"), Nothing),
                            If(row("Hukuman") IsNot DBNull.Value, row("Hukuman"), Nothing),
                            If(row("Keterampilan") IsNot DBNull.Value, row("Keterampilan"), Nothing),
                            0 ' Kolom Total dimulai dengan nilai 0
                        )
                    End If
                Next

                ' Buat kueri SQL untuk mengambil nilai bobot dari tabel bobot
                Dim queryBobot As String = "SELECT nilai_bobot FROM bobot"
                Dim adapterBobot As New MySqlDataAdapter(queryBobot, conn)
                Dim dtBobot As New DataTable()
                adapterBobot.Fill(dtBobot)

                ' Mendapatkan nilai bobot dari tabel bobot
                Dim nilaiBobot As Double() = dtBobot.AsEnumerable().Select(Function(row) Convert.ToDouble(row("nilai_bobot"))).ToArray()

                ' Hitung hasil normalisasi dan perkalian dengan nilai bobot
                For Each row As DataGridViewRow In dgv.Rows
                    Dim total As Double = 0 ' Variabel untuk menyimpan total nilai
                    For Each cell As DataGridViewCell In row.Cells
                        If cell.OwningColumn.Name <> "Alternatif" AndAlso cell.OwningColumn.Name <> "Total" AndAlso cell.OwningColumn.Name <> "Ranking" Then
                            Dim nilai As Double
                            If cell.Value IsNot Nothing AndAlso Not cell.Value.Equals(DBNull.Value) Then
                                nilai = Convert.ToDouble(cell.Value)
                            Else
                                nilai = 0 ' Nilai default jika nilai null
                            End If

                            Dim kolomIndex As Integer = cell.ColumnIndex - 2 ' Indeks untuk mengakses nilai bobot
                            cell.Value = nilai * nilaiBobot(kolomIndex)
                            total += nilai * nilaiBobot(kolomIndex) ' Tambahkan nilai berbobot ke total
                        End If
                    Next
                    row.Cells("Total").Value = total ' Set nilai total pada kolom Total
                Next

                ' Lakukan peringkat berdasarkan total nilai
                dgv.Sort(dgv.Columns("Total"), System.ComponentModel.ListSortDirection.Descending)

                ' Tambahkan peringkat (ranking)
                Dim ranking As Integer = 1
                For i As Integer = 0 To dgv.Rows.Count - 1
                    dgv.Rows(i).Cells("Ranking").Value = ranking
                    If i < dgv.Rows.Count - 1 AndAlso Convert.ToDouble(dgv.Rows(i).Cells("Total").Value) <> Convert.ToDouble(dgv.Rows(i + 1).Cells("Total").Value) Then
                        ranking += 1
                    End If
                Next
            End If

            ' Tutup koneksi
            tutupdb()

        Catch ex As Exception
            ' Tangani kesalahan di sini
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Class DuplicateKeyComparer(Of TKey)
        Implements IComparer(Of TKey)

        Public Function Compare(ByVal x As TKey, ByVal y As TKey) As Integer Implements IComparer(Of TKey).Compare
            Return 0
        End Function
    End Class

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Menampilkan tampilan sebelum mencetak
        Dim printPreviewDialog As New PrintPreviewDialog()
        printPreviewDialog.Document = PrintDocument1
        printPreviewDialog.ShowDialog()
    End Sub

    Private Sub PrintDocument1_PrintPage_1(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim font As New Font("Arial", 12)

        ' Membuat teks yang akan dicetak dari hasil perangkingan
        Dim content As New StringBuilder()
        content.AppendLine("Hasil Peringkat:")
        content.AppendLine()

        ' Membuat tabel untuk menampilkan hasil peringkat
        Dim table As New DataTable()
        table.Columns.Add("Ranking", GetType(String))
        table.Columns.Add("Alternatif", GetType(String))
        table.Columns.Add("Kehadiran", GetType(String))
        table.Columns.Add("Kedisiplinan", GetType(String))
        table.Columns.Add("Kinerja", GetType(String))
        table.Columns.Add("Hukuman", GetType(String))
        table.Columns.Add("Keterampilan", GetType(String))
        table.Columns.Add("Total", GetType(String))

        Dim isFirstRanking As Boolean = True ' Flag untuk menandai peringkat pertama
        Dim firstRankingName As String = "" ' Nama yang mendapatkan peringkat pertama
        For Each row As DataGridViewRow In dgv.Rows
            Dim ranking As String = row.Cells("Ranking").Value.ToString()
            Dim alternatif As String = If(row.Cells("Alternatif").Value IsNot Nothing, row.Cells("Alternatif").Value.ToString(), "")
            Dim kehadiran As String = If(row.Cells("Kehadiran").Value IsNot Nothing, row.Cells("Kehadiran").Value.ToString(), "")
            Dim kedisiplinan As String = If(row.Cells("Kedisiplinan").Value IsNot Nothing, row.Cells("Kedisiplinan").Value.ToString(), "")
            Dim kinerja As String = If(row.Cells("Kinerja").Value IsNot Nothing, row.Cells("Kinerja").Value.ToString(), "")
            Dim hukuman As String = If(row.Cells("Hukuman").Value IsNot Nothing, row.Cells("Hukuman").Value.ToString(), "")
            Dim keterampilan As String = If(row.Cells("Keterampilan").Value IsNot Nothing, row.Cells("Keterampilan").Value.ToString(), "")
            Dim total As String = If(row.Cells("Total").Value IsNot Nothing, row.Cells("Total").Value.ToString(), "")

            If isFirstRanking Then
                firstRankingName = alternatif ' Simpan nama yang mendapatkan peringkat pertama
                isFirstRanking = False ' Set flag menjadi False setelah menemukan peringkat pertama
            End If

            ' Menambahkan baris ke dalam tabel
            table.Rows.Add(ranking, alternatif, kehadiran, kedisiplinan, kinerja, hukuman, keterampilan, total)
        Next

        ' Menggambar tabel di area cetak
        Dim xPos As Integer = 100 ' Mulai menggambar tabel dari posisi horizontal 100 pixel
        Dim yPos As Integer = 100 ' Mulai menggambar tabel dari posisi vertikal 100 pixel
        Dim cellWidth As Integer = 100 ' Lebar setiap sel dalam tabel
        Dim cellHeight As Integer = 20 ' Tinggi setiap sel dalam tabel

        ' Menggambar header kolom
        For Each column As DataColumn In table.Columns
            e.Graphics.DrawString(column.ColumnName, font, Brushes.Black, New RectangleF(xPos, yPos, cellWidth, cellHeight))
            xPos += cellWidth ' Memindahkan posisi horizontal untuk sel berikutnya
        Next

        ' Menggambar isi tabel
        For Each row As DataRow In table.Rows
            xPos = 100 ' Set ulang posisi horizontal ke awal setiap baris
            yPos += cellHeight ' Menambahkan jarak vertikal sebelum menggambar baris baru
            For Each column As DataColumn In table.Columns
                e.Graphics.DrawString(row(column.ColumnName).ToString(), font, Brushes.Black, New RectangleF(xPos, yPos, cellWidth, cellHeight))
                xPos += cellWidth ' Memindahkan posisi horizontal untuk sel berikutnya
            Next
        Next

        ' Memberi ucapan selamat untuk peringkat pertama
        If Not String.IsNullOrEmpty(firstRankingName) Then
            Dim congratulations As String = $"Selamat kepada {firstRankingName} untuk peringkat pertama!"
            yPos += 2 * cellHeight ' Menambahkan jarak vertikal sebelum ucapan selamat
            e.Graphics.DrawString(congratulations, font, Brushes.Black, New PointF(100, yPos))
        End If

        ' Tanda tangan dan tanggal
        Dim signature As String = "Tanda tangan Green Laundry: ____________________________"
        Dim dateText As String = $"Tanggal: {DateTime.Now.ToShortDateString()}"
        Dim signatureHeight As Integer = CInt(e.Graphics.MeasureString(signature, font).Height)
        Dim dateHeight As Integer = CInt(e.Graphics.MeasureString(dateText, font).Height)
        e.Graphics.DrawString(signature, font, Brushes.Black, New PointF(100, e.MarginBounds.Bottom - signatureHeight - 2 * dateHeight))
        e.Graphics.DrawString(dateText, font, Brushes.Black, New PointF(100, e.MarginBounds.Bottom - dateHeight))

        ' Memberi tahu printer bahwa tidak ada halaman lain yang akan dicetak
        e.HasMorePages = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ProsesPeringkat()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
End Class