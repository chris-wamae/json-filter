// See https://aka.ms/new-console-template for more information

using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

var allFoodDataJson = File.ReadAllText("../../../foundationDownload.json");

JObject allFoodDataJsonObject = JObject.Parse(allFoodDataJson);

int id = 0;

var foodsDescriptionsAndCaloriesObject = allFoodDataJsonObject["FoundationFoods"].Select(x =>
{
    id += 1;

    return new
    {
        //Id = id,
        Name = x.Value<string>("description"),
        Description = x.Value<string>("description"),
        EnergyNutrient = new
        {
            //FoodId = id,
            Calories = x.Value<JArray>("foodNutrients")
                .Where(x => x.Value<JObject>("nutrient").Value<string>("unitName") == "kcal")
                .Select(x => x.Value<int>("amount"))
                .FirstOrDefault() + " kcal",
            Starch = x.Value<JArray>("foodNutrients")
                .Where(x => x.Value<JObject>("nutrient").Value<string>("name") == "Starch")
                .Select(x => x.Value<int>("amount"))
                .FirstOrDefault() + " g",
            Lactose = x.Value<JArray>("foodNutrients")
                .Where(x => x.Value<JObject>("nutrient").Value<string>("name") == "Lactose")
                .Select(x => x.Value<int>("amount"))
                .FirstOrDefault() + " g",
            Sugars = x.Value<JArray>("foodNutrients")
                .Where(x => x.Value<JObject>("nutrient").Value<string>("name") == "Sugars, Total")
                .Select(x => x.Value<int>("amount"))
                .FirstOrDefault() + " g",
            Carbohydrate = x.Value<JArray>("foodNutrients")
                .Where(
                    x =>
                        x.Value<JObject>("nutrient").Value<string>("name")
                        == "Carbohydrate, by summation"
                )
                .Select(x => x.Value<int>("amount"))
                .FirstOrDefault() + " g"
        },
        Vitamin = new
        {
            //FoodId = id,
            VitaminA = x.Value<JArray>("foodNutrients")
                .Where(x => x.Value<JObject>("nutrient").Value<string>("name") == "Vitamin A, RAE")
                .Select(x => x.Value<int>("amount"))
                .FirstOrDefault() + " ug",
            VitaminB6 = x.Value<JArray>("foodNutrients")
                .Where(x => x.Value<JObject>("nutrient").Value<string>("name") == "Vitamin B-6")
                .Select(x => x.Value<int>("amount"))
                .FirstOrDefault() + " mg",
            VitaminC = x.Value<JArray>("foodNutrients")
                .Where(
                    x =>
                        x.Value<JObject>("nutrient").Value<string>("name")
                        == "Vitamin C, total ascorbic acid"
                )
                .Select(x => x.Value<int>("amount"))
                .FirstOrDefault() + " mg",
        },
        UngroupedNutrient = new
        {
            //FoodId = id,
            Fiber = x.Value<JArray>("foodNutrients")
                .Where(
                    x =>
                        x.Value<JObject>("nutrient").Value<string>("name") == "Fiber, total dietary"
                )
                .Select(x => x.Value<int>("amount"))
                .FirstOrDefault() + " g",
            Water = x.Value<JArray>("foodNutrients")
                .Where(x => x.Value<JObject>("nutrient").Value<string>("name") == "Water")
                .Select(x => x.Value<int>("amount"))
                .FirstOrDefault() + " g",
            Nitrogen = x.Value<JArray>("foodNutrients")
                .Where(x => x.Value<JObject>("nutrient").Value<string>("name") == "Nitrogen")
                .Select(x => x.Value<int>("amount"))
                .FirstOrDefault() + " g",
            Protein = x.Value<JArray>("foodNutrients")
                .Where(x => x.Value<JObject>("nutrient").Value<string>("name") == "Protein")
                .Select(x => x.Value<int>("amount"))
                .FirstOrDefault() + " g"
        },
        Mineral = new
        {
            //FoodId = id,
            Iron = x.Value<JArray>("foodNutrients")
                .Where(x => x.Value<JObject>("nutrient").Value<string>("name") == "Iron, Fe")
                .Select(x => x.Value<int>("amount"))
                .FirstOrDefault() + " mg",
            Magnesium = x.Value<JArray>("foodNutrients")
                .Where(x => x.Value<JObject>("nutrient").Value<string>("name") == "Magnesium, Mg")
                .Select(x => x.Value<int>("amount"))
                .FirstOrDefault() + " mg",
            Calcium = x.Value<JArray>("foodNutrients")
                .Where(x => x.Value<JObject>("nutrient").Value<string>("name") == "Calcium, Ca")
                .Select(x => x.Value<int>("amount"))
                .FirstOrDefault() + " mg",
            Potassium = x.Value<JArray>("foodNutrients")
                .Where(x => x.Value<JObject>("nutrient").Value<string>("name") == "Potassium, K")
                .Select(x => x.Value<int>("amount"))
                .FirstOrDefault() + " mg",
            Zinc = x.Value<JArray>("foodNutrients")
                .Where(x => x.Value<JObject>("nutrient").Value<string>("name") == "Zinc, Zn")
                .Select(x => x.Value<int>("amount"))
                .FirstOrDefault() + " mg",
        }

        //Calories = x.Value<JArray>("foodNutrients").Where(x => x.Value<JObject>("nutrient").Value<string>("unitName") == "kcal").Select(x => x.Value<int>("amount")).FirstOrDefault()
        //.Select(x => x.Value<string>("amount")).FirstOrDefault(),
    };
});

//var foodNtrsz = parsed["FoundationFoods"].Select(x => x.Value<JArray>("foodNutrients").Where(x => x.Value<JObject>("nutrient").Value<string>("unitName") == "kcal").Select(x => x.Value<int>("amount")).FirstOrDefault());
//).Where(x => x.Value<string>("unitName") == "kcal").FirstOrDefault());

//var one = foodNtrsz.First();
//Console.Write(one.Where(x => x.Value<int>("id") == 2219745).FirstOrDefault());


//   .Select(x => x.Value<int>("id"));
//.Select(x => x.Value<string>("nutrient")));

//Console.WriteLine(foodNtrs.First());

//Console.WriteLine(foodNtrs.Count());
File.WriteAllText(
    "../../../../foodDescriptionsAndNutrients.json",
    System.Text.Json.JsonSerializer.Serialize(foodsDescriptionsAndCaloriesObject)
);
