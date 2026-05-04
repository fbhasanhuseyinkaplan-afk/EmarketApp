using System;
namespace EmarketApp.Entities
{
    public class Kampanya
    {
        public int KampanyaID { get; set; }
        public string KampanyaAd { get; set; }
        public decimal IndirimOrani { get; set; }
        public DateTime BaslangicTarih { get; set; }
        public DateTime BitisTarih { get; set; }
    }
}
