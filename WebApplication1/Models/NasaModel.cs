using System.Collections.Generic;

namespace WebApplication1.Models
{
  public class NasaModel
  {
    public List<OutputType> Output { get; set; }
    public List<InputType> Inputs { get; set; }
    public class InputType
    {
      public string RightUpCorner { get; set; }
      public string StartPoint { get; set; }
      public string MovementCode { get; set; }
    }

    public class OutputType
    {
      public string LastPoint { get; set; }
    }

    public string ErrorMessage { get; set; }
  }
}