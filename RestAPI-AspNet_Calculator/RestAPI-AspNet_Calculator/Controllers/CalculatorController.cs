using Microsoft.AspNetCore.Mvc;

namespace RestAPI_AspNet_Calculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        private decimal convertToDecimal(string firstNumber)
        {
            decimal decimalValue;

            if (decimal.TryParse(firstNumber, out decimalValue))
            {
                return decimalValue;
            }

            return 0;
        }

        private bool isNumeric(string firstNumber)
        {
            double Number;

            bool isNumber = double.TryParse(firstNumber, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out Number);

            return isNumber;

        }

        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Sum(string firstNumber, string secondNumber)
        {

            if (isNumeric(firstNumber) && isNumeric(secondNumber))
            {
                var sum = convertToDecimal(firstNumber) + convertToDecimal(secondNumber);

                return Ok(sum.ToString());
            }


            return BadRequest("Invalid Input");
        }

        [HttpGet("subtraction/{firstNumber}/{secondNumber}")]
        public IActionResult Subtraction(string firstNumber, string secondNumber)
        {

            if (isNumeric(firstNumber) && isNumeric(secondNumber))
            {
                var sum = convertToDecimal(firstNumber) - convertToDecimal(secondNumber);

                return Ok(sum.ToString());
            }


            return BadRequest("Invalid Input");
        }


        [HttpGet("division/{firstNumber}/{secondNumber}")]
        public IActionResult Division(string firstNumber, string secondNumber)
        {

            if (isNumeric(firstNumber) && isNumeric(secondNumber))
            {
                var sum = convertToDecimal(firstNumber) / convertToDecimal(secondNumber);

                return Ok(sum.ToString());
            }


            return BadRequest("Invalid Input");
        }


        [HttpGet("mean/{firstNumber}/{secondNumber}")]
        public IActionResult Mean(string firstNumber, string secondNumber)
        {

            if (isNumeric(firstNumber) && isNumeric(secondNumber))
            {
                var Mean = (convertToDecimal(firstNumber) + convertToDecimal(secondNumber)) / 2;


                return Ok(Mean.ToString());
            }


            return BadRequest("Invalid Input");
        }

        [HttpGet("square-root/{firstNumber}")]
        public IActionResult SquaRoot(string firstNumber)
        {

            if (isNumeric(firstNumber))
            {

                var SquareRoot = Math.Sqrt((double)convertToDecimal(firstNumber));


                return Ok(SquareRoot.ToString("f2"));
            }


            return BadRequest("Invalid Input");
        }
    }
}
