using Films.Interfaces;
using Films.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Films.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IFilmsService filmsService;

        public List<Film>? Films { get; set; }

        public List<Category>? TotalCategories { get; set; }
        public List<Actor>? TotalActors { get; set; }
        public List<string>? TotalReleaseYears { get; set; }
        public List<string>? TotalRatings { get; set; }

        [BindProperty]
        public string? Button { get; set; }

        [BindProperty]
        public int CategoryId { get; set; }

        [BindProperty]
        public int ActorId { get; set; }

        [BindProperty]
        public string ReleaseYear { get; set; }

        [BindProperty]
        public decimal MinRentalRate { get; set; }

        [BindProperty]
        public decimal MaxRentalRate { get; set; }

        [BindProperty]
        public Int16 MinLength { get; set; }

        [BindProperty]
        public Int16 MaxLength { get; set; }

        public Int16? PossibleMaxLength { get; set; }

        [BindProperty]
        public string Rating { get; set; }

        [BindProperty]
        public decimal MinReplacementCost { get; set; }

        [BindProperty]
        public decimal MaxReplacementCost { get; set; }

        public decimal PossibleMaxReplacementCost { get; set; }


        public IndexModel(IFilmsService filmsService)
        {
            this.filmsService = filmsService;
        }

        public IActionResult OnGet()
        {
            Films = filmsService.GetAll();
            if (Films != null)
            {
                if (TotalCategories == null || TotalCategories.Count == 0)
                {
                    MakeListOfCategories();
                }
                if (TotalActors == null || TotalActors.Count == 0)
                {
                    MakeListOfActors();
                }
                if (TotalReleaseYears == null || TotalReleaseYears.Count == 0)
                {
                    MakeListOfReleaseYears();
                }
                if (TotalRatings == null || TotalRatings.Count == 0)
                {
                    MakeListOfRatings();
                }
                PossibleMaxLength = (Int16)Films.OrderByDescending(x => x.Length).First().Length!;
                PossibleMaxLength = PossibleMaxLength == null ? (Int16)200 : PossibleMaxLength;
                PossibleMaxReplacementCost = Films.OrderByDescending(x => x.ReplacementCost).First().ReplacementCost;
                var cookieOption = new CookieOptions();
                cookieOption.Expires = DateTime.Now.AddHours(1);
                cookieOption.Path = "/";
                Response.Cookies.Append("PossibleMaxLengthCookie", PossibleMaxLength.ToString()!, cookieOption);
                Response.Cookies.Append("PossibleMaxReplacementCostCookie", PossibleMaxReplacementCost.ToString().Replace(',', '.')!, cookieOption);
            }
            CategoryId = 0;
            ActorId = 0;
            ReleaseYear = "All";
            MinRentalRate = 0;
            MaxRentalRate = 5;
            MinLength = 0;
            MaxLength = (Int16)PossibleMaxLength!;
            Rating = "All";
            MinReplacementCost = 0;
            MaxReplacementCost = PossibleMaxReplacementCost;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (Button == "filter")
            {
                if (CategoryId == 0
                    && ActorId == 0
                    && ReleaseYear == "All"
                    && MinRentalRate == 0
                    && MaxRentalRate == 5
                    && MinLength == 0
                    && MaxLength == Int16.Parse(Request.Cookies["PossibleMaxLengthCookie"]!)
                    && Rating == "All"
                    && MinReplacementCost == 0
                    && MaxReplacementCost == decimal.Parse(Request.Cookies["PossibleMaxReplacementCostCookie"]!))
                {
                    return OnGet();
                }
                Films = filmsService.GetAll();
                if (CategoryId != 0)
                {
                    var result = new List<Film>();
                    foreach (var film in Films!)
                    {
                        if (film.Categories!.Any(x => x.CategoryId == CategoryId))
                        {
                            result.Add(film);
                        }
                    }
                    Films = result;
                }
                if (ActorId != 0)
                {
                    var result = new List<Film>();
                    foreach (var film in Films!)
                    {
                        if (film.Actors!.Any(x => x.ActorId == ActorId))
                        {
                            result.Add(film);
                        }
                    }
                    Films = result;
                }
                if (ReleaseYear != "All")
                {
                    var result = new List<Film>();
                    foreach (var film in Films!)
                    {
                        if (film.ReleaseYear == ReleaseYear)
                        {
                            result.Add(film);
                        }
                    }
                    Films = result;
                }
                if (MinRentalRate != 0 || MaxRentalRate != 5)
                {
                    var result = new List<Film>();
                    foreach (var film in Films!)
                    {
                        if (film.RentalRate > MinRentalRate && film.RentalRate < MaxRentalRate)
                        {
                            result.Add(film);
                        }
                    }
                    Films = result;
                }
                if (MinLength != 0 || MaxLength != Int16.Parse(Request.Cookies["PossibleMaxLengthCookie"]!))
                {
                    var result = new List<Film>();
                    foreach (var film in Films!)
                    {
                        if (film.Length > MinLength && film.Length < MaxLength)
                        {
                            result.Add(film);
                        }
                    }
                    Films = result;
                }
                if (Rating != "All")
                {
                    var result = new List<Film>();
                    foreach (var film in Films!)
                    {
                        if (film.Rating == Rating)
                        {
                            result.Add(film);
                        }
                    }
                    Films = result;
                }
                if (MinReplacementCost != 0 || MaxReplacementCost != decimal.Parse(Request.Cookies["PossibleMaxReplacementCostCookie"]!))
                {
                    var result = new List<Film>();
                    foreach (var film in Films!)
                    {
                        if (film.ReplacementCost > MinReplacementCost && film.ReplacementCost < MaxReplacementCost)
                        {
                            result.Add(film);
                        }
                    }
                    Films = result;
                }
                if (Films != null)
                {
                    MakeListOfCategories();
                    MakeListOfActors();
                    MakeListOfReleaseYears();
                    MakeListOfRatings();
                }
            }
            if (Button == "clear")
            {
                Films = filmsService.GetAll();
                CategoryId = 0;
                ActorId = 0;
                ReleaseYear = "All";
                MinRentalRate = 0;
                MaxRentalRate = 5;
                if (Films != null)
                {
                    MakeListOfCategories();
                    MakeListOfActors();
                    MakeListOfReleaseYears();
                    MakeListOfRatings();
                    PossibleMaxLength = (Int16)Films.OrderByDescending(x => x.Length).First().Length!;
                    PossibleMaxLength = PossibleMaxLength == null ? (Int16)200 : PossibleMaxLength;
                    PossibleMaxReplacementCost = Films.OrderByDescending(x => x.ReplacementCost).First().ReplacementCost;
                }
                MinLength = 0;
                MaxLength = (Int16)PossibleMaxLength!;
                Rating = "All";
                MinReplacementCost = 0;
                MaxReplacementCost = PossibleMaxReplacementCost;
            }
            return Page();
        }

        private void MakeListOfCategories()
        {
            TotalCategories = new List<Category>();
            foreach (var film in Films!)
            {
                if (film.Categories != null)
                {
                    foreach (var category in film.Categories)
                    {
                        if (TotalCategories.Count == 0)
                        {
                            TotalCategories.Add(category);
                            continue;
                        }
                        bool noSuchCategory = true;
                        foreach (var totalCategory in TotalCategories)
                        {
                            if (category == totalCategory)
                            {
                                noSuchCategory = false;
                                break;
                            }
                        }
                        if (noSuchCategory)
                        {
                            TotalCategories.Add(category);
                        }
                    }
                }
            }
            TotalCategories.Sort((a, b) => a.Name.CompareTo(b.Name));
            TotalCategories.Insert(0, new Category() { CategoryId = 0, Name = "All" });
        }

        private void MakeListOfActors()
        {
            TotalActors = new List<Actor>();
            foreach (var film in Films!)
            {
                if (film.Actors != null)
                {
                    foreach (var actor in film.Actors)
                    {
                        if (TotalActors.Count == 0)
                        {
                            TotalActors.Add(actor);
                            continue;
                        }
                        bool noSuchActor = true;
                        foreach (var totalActor in TotalActors)
                        {
                            if (actor == totalActor)
                            {
                                noSuchActor = false;
                                break;
                            }
                        }
                        if (noSuchActor)
                        {
                            TotalActors.Add(actor);
                        }
                    }
                }
            }
            TotalActors.Sort((a, b) =>
            {
                var result = a.FirstName.CompareTo(b.FirstName);
                return result != 0 ? result : a.LastName.CompareTo(b.LastName);
            });
            TotalActors.Insert(0, new Actor() { ActorId = 0, FirstName = "All", LastName = "" });
        }

        private void MakeListOfReleaseYears()
        {
            TotalReleaseYears = new List<string>();
            foreach (var film in Films!)
            {
                if (film.ReleaseYear != null)
                {
                    if (TotalReleaseYears.Count == 0)
                    {
                        TotalReleaseYears.Add(film.ReleaseYear);
                        continue;
                    }
                    bool noSuchYear = true;
                    foreach (var totalReleaseYear in TotalReleaseYears)
                    {
                        if (film.ReleaseYear == totalReleaseYear)
                        {
                            noSuchYear = false;
                            break;
                        }
                    }
                    if (noSuchYear)
                    {
                        TotalReleaseYears.Add(film.ReleaseYear);
                    }
                }
            }
            TotalReleaseYears.Sort();
            TotalReleaseYears.Insert(0, "All");
        }

        private void MakeListOfRatings()
        {
            TotalRatings = new List<string>();
            foreach (var film in Films!)
            {
                if (film.Rating != null)
                {
                    if (TotalRatings.Count == 0)
                    {
                        TotalRatings.Add(film.Rating);
                        continue;
                    }
                    bool noSuchRating = true;
                    foreach (var totalRating in TotalRatings)
                    {
                        if (film.Rating == totalRating)
                        {
                            noSuchRating = false;
                            break;
                        }
                    }
                    if (noSuchRating)
                    {
                        TotalRatings.Add(film.Rating);
                    }
                }
            }
            TotalRatings.Sort();
            TotalRatings.Insert(0, "All");
        }
    }
}