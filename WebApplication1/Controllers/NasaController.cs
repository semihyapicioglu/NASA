using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
  public class NasaController : Controller
  {
    [HttpGet]
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult GetOutput(NasaModel model)
    {
      model.Output = new List<NasaModel.OutputType>();
      try
      {
        for (int i = 0; i < 2; i++)
        {
          var surfaceXCoordinat = model.Inputs[0].RightUpCorner.Split(' ')[0];
          var surfaceYCoordinat = model.Inputs[0].RightUpCorner.Split(' ')[1];

          var startPointXCoordinat = model.Inputs[i].StartPoint.Split(' ')[0];
          var startPointYCoordinat = model.Inputs[i].StartPoint.Split(' ')[1];
          var startPointDirection = model.Inputs[i].StartPoint.Split(' ')[2];

          var currentDirection = startPointDirection;
          var currentXCoordinat = Convert.ToInt32(startPointXCoordinat);
          var currentYCoordinat = Convert.ToInt32(startPointYCoordinat);

          foreach (char c in model.Inputs[i].MovementCode)
          {
            switch (c)
            {
              case 'M':
                switch (currentDirection)
                {
                  case "N":
                    currentYCoordinat++;
                    break;
                  case "E":
                    currentXCoordinat++;
                    break;
                  case "S":
                    currentYCoordinat--;
                    break;
                  case "W":
                    currentXCoordinat--;
                    break;
                }
                break;

              case 'L':
                switch (currentDirection)
                {
                  case "N":
                    currentDirection = "W";
                    break;
                  case "E":
                    currentDirection = "N";
                    break;
                  case "S":
                    currentDirection = "E";
                    break;
                  case "W":
                    currentDirection = "S";
                    break;
                }
                break;

              case 'R':
                switch (currentDirection)
                {
                  case "N":
                    currentDirection = "E";
                    break;
                  case "E":
                    currentDirection = "S";
                    break;
                  case "S":
                    currentDirection = "W";
                    break;
                  case "W":
                    currentDirection = "N";
                    break;
                }
                break;

            }
          }

          if (currentXCoordinat > Convert.ToInt32(surfaceXCoordinat))
          {
            currentXCoordinat = Convert.ToInt32(surfaceXCoordinat);
          }
          else if (currentXCoordinat < 0)
          {
            currentXCoordinat = 0;
          }

          if (currentYCoordinat > Convert.ToInt32(surfaceYCoordinat))
          {
            currentYCoordinat = Convert.ToInt32(surfaceYCoordinat);
          }
          else if (currentYCoordinat < 0)
          {
            currentYCoordinat = 0;
          }

          var output = new NasaModel.OutputType()
          {
            LastPoint = currentXCoordinat + " " + currentYCoordinat + " " + currentDirection
          };
          model.Output.Add(output);
        }

      }
      catch (Exception ex)
      {
        model.ErrorMessage = "Lütfen parametreleri doğru girdiğinizden emin olunuz.";
      }

      return Json(model);
    }
  }
}