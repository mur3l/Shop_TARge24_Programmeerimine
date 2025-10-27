using System;
using System.Collections.Generic;

namespace ShopTARge24.Models.CockTails
{
    public class CockTailsModel
    {
            public string idDrink { get; set; }
            public string strDrink { get; set; }
            public string strCategory { get; set; }
            public string strIBA { get; set; }
            public string strAlcoholic { get; set; }
            public string strGlass { get; set; }
            public string strInstructions { get; set; }
            public string strDrinkThumb { get; set; }

            public string strIngredient1 { get; set; }
            public string strIngredient2 { get; set; }
            public string strIngredient3 { get; set; }
            public string strIngredient4 { get; set; }
            public string strIngredient5 { get; set; }
            public string strMeasure1 { get; set; }
            public string strMeasure2 { get; set; }
            public string strMeasure3 { get; set; }
            public string strMeasure4 { get; set; }
            public string strMeasure5 { get; set; }
            public string strImageSource { get; set; }
            public string strImageAttribution { get; set; }
            public string dateModified { get; set; }
            public string Value { get; set; }
    }

        public class Root
        {
            public List<CockTailsModel> drinks { get; set; }
        }
    }