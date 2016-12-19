using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroPallets.Models
{
    public class Filter
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<string> Name_Choisen { get; set; }

        public static List<Filter> GetAll()
        {
            return new List<Filter>()
            {
                new Filter() {Id=1,Name="Цена",Name_Choisen = new List<string>() {"Всички","100 - 150","150-300","300-500","500-1000","над 1000" } },
                new Filter() {Id=2,Name="Категория",Name_Choisen = new List<string>() { "Всички", "Маси за кафе","Трапезни маси","Помощни маси" } },
                new Filter() {Id=3,Name="Размер",Name_Choisen = new List<string>() { "Всички", "Малкъ","Среден","Гоялям" } },
                new Filter() {Id=4,Name="Материал",Name_Choisen = new List<string>() { "Всички", "Само дърво","Дърво и стъкло","Дърво и друго" } },
                new Filter() {Id=5,Name="Цвят",Name_Choisen = new List<string>() { "Всички", "Необработен","Боядисан","Лакиран" } },
            };
        }
    }
}
