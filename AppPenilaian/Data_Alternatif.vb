Imports MySql.Data.MySqlClient

Public Class Data_Alternatif
    Private Sub Data_Alternatif_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampil()
        tampildata()
    End Sub
    Sub tampildata()
        Try
            Call koneksi()

            ' Mengisi dataset dengan data dari tabel data_alternatif
            da = New MySqlDataAdapter("SELECT * FROM data_alternatif", conn)
            ds = New DataSet
            da.Fill(ds, "data_alternatif")

            ' Menetapkan sumber data DataGridView ke tabel data_alternatif dalam dataset
            dgv2.DataSource = ds.Tables("data_alternatif")

            ' Mengatur mode pilihan menjadi sel-sel tunggal
            dgv2.SelectionMode = DataGridViewSelectionMode.FullRowSelect

            ' Mengatur agar pengguna dapat memilih lebih dari satu baris
            dgv2.MultiSelect = False

            ' Menutup koneksi
            tutupdb()

        Catch ex As Exception
            ' Tangani kesalahan di sini
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub tampil()
        Try
            Call koneksi()
            ' Buat kueri SQL untuk mengambil data dari tabel karyawan
            Dim queryKaryawan As String = "SELECT id, nama FROM karyawan"
            Dim adapterKaryawan As New MySqlDataAdapter(queryKaryawan, conn)
            Dim dtKaryawan As New DataTable()
            adapterKaryawan.Fill(dtKaryawan)

            ' Buat kolom ID dan Nama untuk DataGridView
            dgv.Columns.Add("Kode Alternatif", "Kode Alternatif")
            dgv.Columns.Add("Nama", "Nama")

            ' Tambahkan data karyawan ke DataGridView
            For Each row As DataRow In dtKaryawan.Rows
                dgv.Rows.Add(row("id"), row("nama"))
            Next

            ' Nonaktifkan baris kosong untuk menambahkan data
            dgv.AllowUserToAddRows = False

            ' Buat kueri SQL untuk mengambil data nama_kriteria dari tabel kriteria
            Dim queryKriteria As String = "SELECT DISTINCT nama_kriteria FROM kriteria"
            Dim adapterKriteria As New MySqlDataAdapter(queryKriteria, conn)
            Dim dtKriteria As New DataTable()
            adapterKriteria.Fill(dtKriteria)

            ' Buat dictionary untuk melacak apakah kolom combo box sudah ditambahkan untuk setiap kriteria
            Dim addedColumns As New Dictionary(Of String, Boolean)()

            ' Tambahkan kolom nilai dari tabel crips untuk setiap nama_kriteria
            For Each rowKriteria As DataRow In dtKriteria.Rows
                Dim namaKriteria As String = rowKriteria("nama_kriteria").ToString()

                ' Periksa apakah kolom combo box sudah ditambahkan untuk kriteria ini
                If Not addedColumns.ContainsKey(namaKriteria) Then
                    ' Tandai bahwa kolom combo box sudah ditambahkan untuk kriteria ini
                    addedColumns.Add(namaKriteria, True)

                    ' Buat kolom combo box untuk nilai
                    Dim combo As New DataGridViewComboBoxColumn()
                    combo.HeaderText = namaKriteria
                    combo.Name = "Combo_" & namaKriteria

                    ' Tambahkan data nilai dari tabel crips ke combo box
                    Dim queryNilai As String = "SELECT DISTINCT kode,crips_kriteria,nilai FROM crips,kriteria WHERE nama_kriteria = @namaKriteria"
                    Dim adapterNilai As New MySqlDataAdapter(queryNilai, conn)
                    adapterNilai.SelectCommand.Parameters.AddWithValue("@namaKriteria", namaKriteria)
                    Dim dtNilai As New DataTable()
                    adapterNilai.Fill(dtNilai)
                    For Each rowNilai As DataRow In dtNilai.Rows
                        Dim kodeNilai As String = rowNilai("kode").ToString()
                        Dim nilai As String = rowNilai("nilai").ToString()
                        Dim crips As String = rowNilai("crips_kriteria").ToString
                        Dim item As String = kodeNilai & " - " & nilai & " - " & crips ' Gabungkan kode dan nilai
                        combo.Items.Add(item)
                    Next

                    ' Tambahkan kolom combo box ke DataGridView
                    dgv.Columns.Add(combo)
                End If
            Next

            ' Tutup koneksi
            tutupdb()


            ' Tutup koneksi
            tutupdb()

        Catch ex As Exception
            ' Tangani kesalahan di sini
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Call koneksi()

            Dim allRowsSaved As Boolean = True ' Flag untuk menentukan apakah semua baris telah berhasil disimpan

            ' Iterasi melalui baris-baris DataGridView
            For Each dgvRow As DataGridViewRow In dgv.Rows
                ' Mendapatkan data dari DataGridView
                Dim kodeAlternatif As String = ""
                Dim alternatif As String = ""

                ' Periksa apakah sel memiliki nilai sebelum mencoba mengonversinya menjadi string
                If dgvRow.Cells("Kode Alternatif").Value IsNot Nothing Then
                    kodeAlternatif = dgvRow.Cells("Kode Alternatif").Value.ToString()
                End If

                If dgvRow.Cells("Nama").Value IsNot Nothing Then
                    alternatif = dgvRow.Cells("Nama").Value.ToString()
                End If

                ' Periksa apakah kode_alternatif sudah ada dalam database
                If KodeAlternatifExists(kodeAlternatif) Then

                    allRowsSaved = False ' Set flag menjadi False karena ada kode_alternatif yang sudah ada
                    Continue For ' Lanjutkan ke baris berikutnya jika kode_alternatif sudah ada
                End If

                ' Inisialisasi dictionary untuk menyimpan nilai berdasarkan nama kriteria
                Dim nilaiDictionary As New Dictionary(Of String, String)()

                ' Iterasi melalui kolom-kolom DataGridView
                Dim dataIncomplete As Boolean = False ' Flag untuk menandai data belum lengkap
                For Each dgvColumn As DataGridViewColumn In dgv.Columns
                    ' Periksa apakah nama kolom mengandung "Combo_"
                    If dgvColumn.Name.StartsWith("Combo_") Then
                        Dim namaKriteria As String = dgvColumn.HeaderText

                        ' Mendapatkan sel dari DataGridView
                        Dim sel As DataGridViewCell = dgvRow.Cells(dgvColumn.Index)

                        ' Periksa apakah sel bukan null dan merupakan jenis sel combo box
                        If TypeOf sel Is DataGridViewComboBoxCell AndAlso sel.Value IsNot Nothing Then
                            ' Mendapatkan nilai dari sel
                            Dim nilai As String = sel.Value.ToString()

                            ' Tambahkan nilai ke dalam dictionary
                            nilaiDictionary.Add(namaKriteria, nilai)
                        Else
                            ' Jika ada sel yang kosong, atur flag dataIncomplete menjadi True
                            dataIncomplete = True
                        End If
                    End If
                Next

                ' Periksa apakah ada data yang belum diisi
                If String.IsNullOrEmpty(kodeAlternatif) OrElse String.IsNullOrEmpty(alternatif) OrElse nilaiDictionary.Count <> dgv.Columns.Count - 2 Then
                    MessageBox.Show("Mohon lengkapi semua kolom sebelum menyimpan data.", "Data Belum Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    allRowsSaved = False ' Set flag menjadi False karena ada data yang belum lengkap
                    Exit For ' Keluar dari loop jika ada data yang belum diisi
                End If

                ' Contoh menyimpan data ke dalam tabel data_alternatif
                Dim querySimpan As String = "INSERT INTO data_alternatif (kode_alternatif, alternatif, kehadiran, kedisiplinan, kinerja, hukuman, keterampilan) VALUES (@kodeAlternatif, @alternatif, @kehadiran, @kedisiplinan, @kinerja, @hukuman, @keterampilan)"
                Dim cmd As New MySqlCommand(querySimpan, conn)
                cmd.Parameters.AddWithValue("@kodeAlternatif", kodeAlternatif)
                cmd.Parameters.AddWithValue("@alternatif", alternatif)
                cmd.Parameters.AddWithValue("@kehadiran", If(nilaiDictionary.ContainsKey("Kehadiran"), nilaiDictionary("Kehadiran"), ""))
                cmd.Parameters.AddWithValue("@kedisiplinan", If(nilaiDictionary.ContainsKey("Kedisiplinan"), nilaiDictionary("Kedisiplinan"), ""))
                cmd.Parameters.AddWithValue("@kinerja", If(nilaiDictionary.ContainsKey("Kinerja"), nilaiDictionary("Kinerja"), ""))
                cmd.Parameters.AddWithValue("@hukuman", If(nilaiDictionary.ContainsKey("Hukuman"), nilaiDictionary("Hukuman"), ""))
                cmd.Parameters.AddWithValue("@keterampilan", If(nilaiDictionary.ContainsKey("Keterampilan"), nilaiDictionary("Keterampilan"), ""))
                cmd.ExecuteNonQuery()
            Next

            ' Tampilkan pesan jika semua baris berhasil disimpan atau jika data sudah ada
            If allRowsSaved Then
                MessageBox.Show("Data berhasil disimpan.")
            Else
                MessageBox.Show("Semua data sudah ada dalam database. Tidak perlu menyimpan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            ' Tutup koneksi
            tampildata()
            tutupdb()

        Catch ex As Exception
            ' Tangani kesalahan
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        End Try
    End Sub



    ' Fungsi untuk memeriksa apakah kode_alternatif sudah ada dalam database
    Private Function KodeAlternatifExists(kodeAlternatif As String) As Boolean
        Dim queryCheck As String = "SELECT COUNT(*) FROM data_alternatif WHERE kode_alternatif = @kodeAlternatif"
        Using cmd As New MySqlCommand(queryCheck, conn)
            cmd.Parameters.AddWithValue("@kodeAlternatif", kodeAlternatif)
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            Return count > 0
        End Using
    End Function


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Call koneksi()

            Dim dataUpdated As Boolean = False ' Flag untuk menentukan apakah ada data yang di-update

            ' Iterasi melalui baris-baris DataGridView
            For Each dgvRow As DataGridViewRow In dgv.Rows
                ' Mendapatkan data dari DataGridView
                Dim kodeAlternatif As String = ""
                Dim alternatif As String = ""

                ' Periksa apakah sel memiliki nilai sebelum mencoba mengonversinya menjadi string
                If dgvRow.Cells("Kode Alternatif").Value IsNot Nothing Then
                    kodeAlternatif = dgvRow.Cells("Kode Alternatif").Value.ToString()
                End If

                If dgvRow.Cells("Nama").Value IsNot Nothing Then
                    alternatif = dgvRow.Cells("Nama").Value.ToString()
                End If

                ' Inisialisasi dictionary untuk menyimpan nilai berdasarkan nama kriteria
                Dim nilaiDictionary As New Dictionary(Of String, String)()

                ' Iterasi melalui kolom-kolom DataGridView
                For Each dgvColumn As DataGridViewColumn In dgv.Columns
                    ' Periksa apakah nama kolom mengandung "Combo_"
                    If dgvColumn.Name.StartsWith("Combo_") Then
                        Dim namaKriteria As String = dgvColumn.HeaderText

                        ' Mendapatkan sel dari DataGridView
                        Dim sel As DataGridViewCell = dgvRow.Cells(dgvColumn.Index)

                        ' Periksa apakah sel bukan null dan merupakan jenis sel combo box
                        If TypeOf sel Is DataGridViewComboBoxCell AndAlso sel.Value IsNot Nothing Then
                            ' Mendapatkan nilai dari sel
                            Dim nilai As String = sel.Value.ToString()

                            ' Tambahkan nilai ke dalam dictionary
                            nilaiDictionary.Add(namaKriteria, nilai)
                        End If
                    End If
                Next

                ' Periksa apakah kode_alternatif sudah ada dalam database
                If KodeAlternatifExists(kodeAlternatif) Then
                    ' Periksa apakah data yang akan di-update sama dengan data yang ada di database
                    If Not IsDataUnchanged(kodeAlternatif, alternatif, nilaiDictionary) Then
                        ' Lakukan update data
                        UpdateData(kodeAlternatif, alternatif, nilaiDictionary)
                        dataUpdated = True ' Set flag menjadi True karena ada data yang di-update
                    End If
                End If
            Next

            ' Periksa apakah ada data yang di-update dan tidak kosong
            If dataUpdated Then
                MessageBox.Show("Data berhasil diupdate.")
            Else
                MessageBox.Show("Tidak ada data yang diubah.")
            End If

            ' Tutup koneksi
            tampildata()
            tutupdb()

        Catch ex As Exception
            ' Tangani kesalahan
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        End Try
    End Sub

    ' Fungsi untuk memeriksa apakah data yang akan di-update berubah
    Private Function IsDataUnchanged(kodeAlternatif As String, alternatif As String, nilaiDictionary As Dictionary(Of String, String)) As Boolean
        ' Query untuk mengambil data dari database berdasarkan kode_alternatif
        Dim queryGetData As String = "SELECT * FROM data_alternatif WHERE kode_alternatif = @kodeAlternatif"
        Using cmd As New MySqlCommand(queryGetData, conn)
            cmd.Parameters.AddWithValue("@kodeAlternatif", kodeAlternatif)
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    ' Bandingkan data yang baru dengan data yang ada di database
                    If reader("alternatif").ToString() = alternatif Then
                        For Each pair As KeyValuePair(Of String, String) In nilaiDictionary
                            Dim columnName As String = pair.Key
                            Dim columnValue As String = pair.Value
                            If reader(columnName).ToString() <> columnValue Then
                                Return False ' Data berubah
                            End If
                        Next
                        Return True ' Data tidak berubah
                    End If
                End If
            End Using
        End Using
        Return False ' Data berubah
    End Function



    ' Fungsi untuk menentukan apakah data harus di-update
    Private Function ShouldUpdateData(kodeAlternatif As String, alternatif As String, nilaiDictionary As Dictionary(Of String, String)) As Boolean
        Dim queryCheck As String = "SELECT * FROM data_alternatif WHERE kode_alternatif = @kodeAlternatif"
        Using cmd As New MySqlCommand(queryCheck, conn)
            cmd.Parameters.AddWithValue("@kodeAlternatif", kodeAlternatif)
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    ' Bandingkan nilai yang ada di database dengan nilai baru
                    If alternatif <> reader("alternatif").ToString() Then
                        Return True ' Update jika nilai alternatif berbeda
                    End If

                    For Each kvp As KeyValuePair(Of String, String) In nilaiDictionary
                        Dim kriteria As String = kvp.Key
                        Dim nilai As String = kvp.Value

                        If nilai <> reader(kriteria).ToString() Then
                            Return True ' Update jika nilai kriteria berbeda
                        End If
                    Next
                End If
            End Using
        End Using
        Return False ' Tidak perlu update jika semua nilai sama
    End Function

    ' Fungsi untuk melakukan update data
    Private Sub UpdateData(kodeAlternatif As String, alternatif As String, nilaiDictionary As Dictionary(Of String, String))
        Dim queryUpdate As String = "UPDATE data_alternatif SET alternatif = @alternatif, kehadiran = @kehadiran, kedisiplinan = @kedisiplinan, kinerja = @kinerja, hukuman = @hukuman, keterampilan = @keterampilan WHERE kode_alternatif = @kodeAlternatif"
        Using cmd As New MySqlCommand(queryUpdate, conn)
            cmd.Parameters.AddWithValue("@kodeAlternatif", kodeAlternatif)
            cmd.Parameters.AddWithValue("@alternatif", alternatif)
            cmd.Parameters.AddWithValue("@kehadiran", If(nilaiDictionary.ContainsKey("Kehadiran"), nilaiDictionary("Kehadiran"), ""))
            cmd.Parameters.AddWithValue("@kedisiplinan", If(nilaiDictionary.ContainsKey("Kedisiplinan"), nilaiDictionary("Kedisiplinan"), ""))
            cmd.Parameters.AddWithValue("@kinerja", If(nilaiDictionary.ContainsKey("Kinerja"), nilaiDictionary("Kinerja"), ""))
            cmd.Parameters.AddWithValue("@hukuman", If(nilaiDictionary.ContainsKey("Hukuman"), nilaiDictionary("Hukuman"), ""))
            cmd.Parameters.AddWithValue("@keterampilan", If(nilaiDictionary.ContainsKey("Keterampilan"), nilaiDictionary("Keterampilan"), ""))
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            ' Pastikan ada baris yang dipilih
            If dgv2.SelectedRows.Count > 0 Then
                ' Konfirmasi penghapusan
                Dim result As DialogResult = MessageBox.Show("Anda yakin ingin menghapus baris terpilih?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                ' Jika pengguna mengkonfirmasi untuk menghapus
                If result = DialogResult.Yes Then
                    Call koneksi()

                    ' Iterasi melalui baris-baris yang dipilih secara terbalik untuk menghindari perubahan indeks saat menghapus
                    For i As Integer = dgv2.SelectedRows.Count - 1 To 0 Step -1
                        Dim kodeAlternatif As String = dgv2.SelectedRows(i).Cells("kode_alternatif").Value.ToString()

                        ' Buat kueri SQL untuk menghapus data_alternatif berdasarkan kode_alternatif
                        Dim queryHapus As String = "DELETE FROM data_alternatif WHERE kode_alternatif = @kodeAlternatif"
                        Using cmd As New MySqlCommand(queryHapus, conn)
                            cmd.Parameters.AddWithValue("@kodeAlternatif", kodeAlternatif)
                            cmd.ExecuteNonQuery()
                        End Using

                        ' Hapus baris dari DataGridView
                        dgv2.Rows.RemoveAt(dgv2.SelectedRows(i).Index)
                    Next

                    ' Tutup koneksi
                    tutupdb()

                    MessageBox.Show("Data berhasil dihapus.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                MessageBox.Show("Silakan pilih baris yang ingin dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            ' Tangani kesalahan di sini
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub
End Class
