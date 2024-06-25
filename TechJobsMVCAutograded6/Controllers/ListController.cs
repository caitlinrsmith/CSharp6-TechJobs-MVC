using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechJobsMVCAutograded6.Data;
using TechJobsMVCAutograded6.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsMVCAutograded6.Controllers;

public class ListController : Controller
{
    //  a constructor that populates ColumnChoices and TableChoices with values

    internal static Dictionary<string, string> ColumnChoices = new Dictionary<string, string>()
    {
        { "all", "All" },
        { "employer", "Employer" },
        { "location", "Location" },
        { "positionType", "Position Type" },
        { "coreCompetency", "Skill" }
    };
    internal static Dictionary<string, List<JobField>> TableChoices = new Dictionary<
        string,
        List<JobField>
    >()
    {
        //{"all", "View All"},
        { "employer", JobData.GetAllEmployers() },
        { "location", JobData.GetAllLocations() },
        { "positionType", JobData.GetAllPositionTypes() },
        { "coreCompetency", JobData.GetAllCoreCompetencies() }
    };

    public IActionResult Index()
    {
        // renders a view that displays a table of clickable links for the different job categories.
        // user will arrive at this handler method as a result of submitting a form

        ViewBag.columns = ColumnChoices;
        ViewBag.tableChoices = TableChoices;
        ViewBag.employers = JobData.GetAllEmployers();
        ViewBag.locations = JobData.GetAllLocations();
        ViewBag.positionTypes = JobData.GetAllPositionTypes();
        ViewBag.skills = JobData.GetAllCoreCompetencies();

        return View();
    }

    // TODO #2 - Complete the Jobs action method
    public IActionResult Jobs(string column, string value)
    {
        // this action method renders a different view that displays information for the jobs that relate to a selected category
        // can obtain data by implementing the JobData class methods
        // we are “searching” for a particular value within a particular field and then displaying jobs that match.
        //  user will arrive at this handler method as a result of clicking on a link within the Index.cshtml view

        List<Job> jobs = new();

        if (value == "View All")
        {
            jobs = JobData.FindAll();
        }
        else
        {
            jobs = JobData.FindByColumnAndValue(column, value);
            
        }
        ViewBag.title = value;
        ViewBag.jobs = jobs;

        return View();
    }
}
