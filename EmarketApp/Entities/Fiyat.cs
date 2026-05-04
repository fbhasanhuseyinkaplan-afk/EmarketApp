using System;
namespace EmarketApp.Entities
{
    public class Fiyat
    {
        public int FiyatID { get; set; }
        public int UrunID { get; set; }
        public decimal FiyatDeger { get; set; }
        public DateTime BaslangicTarih { get; set; }
        public DateTime? BitisTarih { get; set; }
    }
}
