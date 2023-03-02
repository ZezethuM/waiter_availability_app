using System.ComponentModel.DataAnnotations;

namespace waitersRazorPages;

public class Shifts
{
    [Required, StringLength(10)]
    public string? FirstName {get; set;}

    public int Week {get; set;}

}