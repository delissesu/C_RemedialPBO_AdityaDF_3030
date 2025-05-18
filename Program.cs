using System;
using System.Collections.Generic;

namespace PerpustakaanMini
{
    interface InterfaceBuku
    {
        void InfoBuku();
        bool Tersedia { get; set; }
    }

    class Buku : InterfaceBuku
    {
        public string Judul;
        public string Penulis;
        public string Tahun;
        public bool Tersedia { get; set; } = true;

        public Buku(string judul, string penulis, string tahun)
        {
            Judul = judul;
            Penulis = penulis;
            Tahun = tahun;
        }

        public virtual void InfoBuku()
        {
            Console.WriteLine($"│ {Judul,-25} │ {Penulis,-20} │ {Tahun,-6} │ {(Tersedia ? "Tersedia" : "Dipinjam"),-10} │");
        }
    }

    class BukuFiksi : Buku
    {
        public string Genre { get; set; }

        public BukuFiksi(string judul, string penulis, string tahun, string genre)
            : base(judul, penulis, tahun)
        {
            Genre = genre;
        }

        public override void InfoBuku()
        {
            Console.WriteLine($"│ {Judul,-25} │ {Penulis,-20} │ {Tahun,-6} │ {(Tersedia ? "Tersedia" : "Dipinjam"),-10} │ {"Fiksi",-10} │ {Genre,-10} │");
        }
    }

    class BukuNonFiksi : Buku
    {
        public string Kategori { get; set; }

        public BukuNonFiksi(string judul, string penulis, string tahun, string kategori)
            : base(judul, penulis, tahun)
        {
            Kategori = kategori;
        }

        public override void InfoBuku()
        {
            Console.WriteLine($"│ {Judul,-25} │ {Penulis,-20} │ {Tahun,-6} │ {(Tersedia ? "Tersedia" : "Dipinjam"),-10} │ {"Non-Fiksi",-10} │ {Kategori,-10} │");
        }
    }

    class Majalah : Buku
    {
        public string Edisi { get; set; }

        public Majalah(string judul, string penulis, string tahun, string edisi)
            : base(judul, penulis, tahun)
        {
            Edisi = edisi;
        }

        public override void InfoBuku()
        {
            Console.WriteLine($"│ {Judul,-25} │ {Penulis,-20} │ {Tahun,-6} │ {(Tersedia ? "Tersedia" : "Dipinjam"),-10} │ {"Majalah",-10} │ {Edisi,-10} │");
        }
    }


    class PeminjamBuku
    {
        public string Nama { get; set; }
        public List<Buku> BukuDipinjam { get; private set; } = new List<Buku>();
        private const int MaksimalPeminjamanBuku = 3;

        public PeminjamBuku(string nama)
        {
            Nama = nama;
        }

        public bool PinjamBuku(Buku buku)
        {
            if (BukuDipinjam.Count >= MaksimalPeminjamanBuku)
            {
                Console.WriteLine($"\nWah! Maaf {Nama}! Batas Peminjaman adalah 3 Buku! Dan Kamu Sudah Meminjam {MaksimalPeminjamanBuku}!");
                return false;
            }

            if (!buku.Tersedia)
            {
                Console.WriteLine($"\nYah! Maaf, Buku '{buku.Judul}' sedang Dipinjam");
                return false;
            }

            buku.Tersedia = false;
            BukuDipinjam.Add(buku);
            Console.WriteLine($"\nHore! Kamu Berhasil Meminjam Buku '{buku.Judul}'");
            return true;
        }

        public bool KembalikanBuku(Buku buku)
        {
            if (!BukuDipinjam.Contains(buku))
            {
                Console.WriteLine($"\nHeum? Kamu tidak Meminjam Buku '{buku.Judul}'");
                return false;
            }

            buku.Tersedia = true;
            BukuDipinjam.Remove(buku);
            Console.WriteLine($"\nHore! Terima Kasih telah Mengembalikan Buku '{buku.Judul}'");
            return true;
        }

        public void LihatBukuDipinjam()
        {
            if (BukuDipinjam.Count == 0)
            {
                Console.WriteLine($"\n{Nama} belum meminjam buku apapun");
                return;
            }

            Console.WriteLine($"\nHalo! Ini adalah Daftar Buku yang Dipinjam oleh {Nama.ToUpper()}!");
            Console.WriteLine("┌───────────────────────────┬──────────────────────┬────────┬────────────┬────────────┬────────────┐");
            Console.WriteLine("│ Judul                     │ Penulis              │ Tahun  │ Status     │ Jenis      │ Genre      │");
            Console.WriteLine("├───────────────────────────┼──────────────────────┼────────┼────────────┼────────────┼────────────┤");

            foreach (var buku in BukuDipinjam)
            {
                buku.InfoBuku();
            }

            Console.WriteLine("└───────────────────────────┴──────────────────────┴────────┴────────────┴────────────┴────────────┘");
        }
    }


    class Program
    {
        static List<Buku> daftarBuku = new List<Buku>();
        static PeminjamBuku peminjam;

        static void Main(string[] args)
        {
            Console.Write("Halo, Pemustakawan! Masukkan Nama Kamu: ");
            peminjam = new PeminjamBuku(Console.ReadLine());
            Console.WriteLine($"Halo {peminjam.Nama}! Selamat Datang di Perpustakaan Gryffindoor!");

            daftarBuku.Add(new BukuFiksi("Harry Potter", "J.K. Rowling", "1997", "Fantasy"));

            bool lanjut = true;
            while (lanjut)
            {
                Console.WriteLine("\nMau ngapain di Perpustakaan?");
                Console.WriteLine("1. Lihat Semua Buku");
                Console.WriteLine("2. Tambah Buku");
                Console.WriteLine("3. Ubah Data Buku");
                Console.WriteLine("4. Pinjam Buku");
                Console.WriteLine("5. Kembalikan Buku");
                Console.WriteLine("6. Lihat Buku yang Dipinjam");
                Console.WriteLine("0. Keluar");
                Console.Write("\nPilih menu (0-6): ");

                string pilihan = Console.ReadLine();
                Console.WriteLine();

                switch (pilihan)
                {
                    case "1": LihatSemuaBuku(); break;
                    case "2": TambahBuku(); break;
                    case "3": UbahDataBuku(); break;
                    case "4": PinjamBuku(); break;
                    case "5": KembalikanBuku(); break;
                    case "6": peminjam.LihatBukuDipinjam(); break;
                    case "0":
                        lanjut = false;
                        Console.WriteLine($"Terima Kasih sudah Datang ke Perpustakaan Gryffindoor!");
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid");
                        break;
                }
            }
        }
        static void LihatSemuaBuku()
        {
            if (daftarBuku.Count == 0)
            {
                Console.WriteLine("Perpustakaan kosong");
                return;
            }

            Console.WriteLine("─── Daftar Buku di Perpustakaan Gryffindoor ───");
            Console.WriteLine("┌───────────────────────────┬──────────────────────┬────────┬────────────┬────────────┬────────────┐");
            Console.WriteLine("│ Judul                     │ Penulis              │ Tahun  │ Status     │ Jenis      │ Info       │");
            Console.WriteLine("├───────────────────────────┼──────────────────────┼────────┼────────────┼────────────┼────────────┤");

            foreach (var buku in daftarBuku)
            {
                buku.InfoBuku();
            }

            Console.WriteLine("└───────────────────────────┴──────────────────────┴────────┴────────────┴────────────┴────────────┘");
        }

        static void TambahBuku()
        {
            Console.WriteLine("─── Mau Tambah Buku Jenis Apa? ───");
            Console.WriteLine("1. Buku Fiksi");
            Console.WriteLine("2. Buku Non-Fiksi");
            Console.WriteLine("3. Majalah");
            Console.Write("Pilih Jenis Buku (1-3): ");
            string jenis = Console.ReadLine();

            Console.Write("Masukkan Judul Buku: ");
            string judul = Console.ReadLine();
            Console.Write("Masukkan Nama Penulis: ");
            string penulis = Console.ReadLine();
            Console.Write("Masukkan Tahun Terbit: ");
            string tahun = Console.ReadLine();

            switch (jenis)
            {
                case "1":
                    Console.Write("Genre: ");
                    string genre = Console.ReadLine();
                    daftarBuku.Add(new BukuFiksi(judul, penulis, tahun, genre));
                    Console.WriteLine($"\nHore! Buku Fiksi berjudul '{judul}' Berhasil Ditambahkan");
                    break;
                case "2":
                    Console.Write("Kategori: ");
                    string kategori = Console.ReadLine();
                    daftarBuku.Add(new BukuNonFiksi(judul, penulis, tahun, kategori));
                    Console.WriteLine($"\nHore! Buku Non-Fiksi berjudul '{judul}' Berhasil Ditambahkan");
                    break;
                case "3":
                    Console.Write("Edisi: ");
                    string edisi = Console.ReadLine();
                    daftarBuku.Add(new Majalah(judul, penulis, tahun, edisi));
                    Console.WriteLine($"\nHore! Majalah berjudul '{judul}' Berhasil Ditambahkan");
                    break;
                default:
                    Console.WriteLine("\nJenis buku tidak valid");
                    break;
            }
        }

        static void UbahDataBuku()
        {
            if (daftarBuku.Count == 0)
            {
                Console.WriteLine("Perpustakaan kosong");
                return;
            }

            Console.WriteLine("Mau Ubah Data Buku atau Majalah? Yuk Ikutin Instruksinya!");
            Console.Write("Masukkan Judul Buku atau Majalah yang Ingin Diubah: ");
            string judul = Console.ReadLine();

            Buku buku = CariBuku(judul);
            if (buku != null)
            {
                Console.Write("Masukkan Judul Baru: ");
                buku.Judul = Console.ReadLine();
                Console.Write("Masukkan Nama Penulis: ");
                buku.Penulis = Console.ReadLine();
                Console.Write("Masukkan Tahun Terbit: ");
                buku.Tahun = Console.ReadLine();
                Console.WriteLine($"\nHore! Data Buku Berhasil Diubah!");
            }
            else
            {
                Console.WriteLine($"\nBuku yang bejudul '{judul}' tidak ada pada daftar buku!");
            }
        }

        static void PinjamBuku()
        {
            if (daftarBuku.Count == 0)
            {
                Console.WriteLine("Perpustakaan kosong");
                return;
            }

            Console.WriteLine("Mau Pinjam Buku? Yuk Ikutin Instruksinya!");
            Console.Write("Masukkan Judul Buku yang Ingin Dipinjam: ");
            string judul = Console.ReadLine();

            Buku buku = CariBuku(judul);
            if (buku != null)
            {
                peminjam.PinjamBuku(buku);
            }
            else
            {
                Console.WriteLine($"\nBuku yang bejudul '{judul}' tidak ada pada daftar buku!");
            }
        }

        static void KembalikanBuku()
        {
            if (peminjam.BukuDipinjam.Count == 0)
            {
                Console.WriteLine($"{peminjam.Nama} belum meminjam buku apapun.");
                return;
            }

            Console.WriteLine("Mau Kembaliin Buku? Yuk Ikutin Instruksinya!");
            Console.Write("Masukkan Judul Buku yang Ingin Dikembalikan: ");
            string judul = Console.ReadLine();

            Buku buku = CariBuku(judul);
            if (buku != null)
            {
                peminjam.KembalikanBuku(buku);
            }
            else
            {
                Console.WriteLine($"\nBuku yang bejudul '{judul}' tidak ada pada daftar buku!");
            }
        }

        static Buku CariBuku(string judul)
        {
            foreach (var buku in daftarBuku)
            {
                if (buku.Judul.ToLower() == judul.ToLower())
                {
                    return buku;
                }
            }
            return null;
        }
    }
}