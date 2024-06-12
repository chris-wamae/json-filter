// See https://aka.ms/new-console-template for more information

using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

var allFoodDataJson = File.ReadAllText("../../../foundationDownload.json");

JObject allFoodDataJsonObject = JObject.Parse(allFoodDataJson);

var foodsDescriptionsAndCaloriesObject = allFoodDataJsonObject["FoundationFoods"].Select(x =>
{
    return new {
        Description = x.Value<string>("description"),
        Calories = x.Value<JArray>("foodNutrients").Where(x => x.Value<JObject>("nutrient").Value<string>("unitName") == "kcal").Select(x => x.Value<int>("amount")).FirstOrDefault()
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
File.WriteAllText("../../../../foodDescriptionsAndCalories.json", System.Text.Json.JsonSerializer.Serialize(foodsDescriptionsAndCaloriesObject));



