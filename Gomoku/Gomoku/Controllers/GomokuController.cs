using Gomoku.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gomoku.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GomokuController : ControllerBase
    {
        private readonly ILogger<GomokuController> _logger;

        /// <summary>
        /// Constructor for GomokuController
        /// </summary>
        /// <param name="logger"></param>
        public GomokuController(ILogger<GomokuController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Places the stone on a target coordinate by the current player
        /// </summary>
        /// <param name="request">Current player and target coordinates</param>
        /// <returns>Result of this turn</returns>
        [HttpPost("PlaceAStone")]
        public string PlaceAStone(PlaceAStoneRequestModel request)
        {
            var gomoku = Objects.Gomoku.Instance;

            try
            {
                return gomoku.PlaceAStone(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ex.Message;
            }
        }

        /// <summary>
        /// Utility API to help visualize the current board status
        /// </summary>
        /// <returns>Board status display</returns>
        [HttpGet("DrawBoard")]
        public string DrawBoard()
        {
            var gomoku = Objects.Gomoku.Instance;

            return gomoku.DrawBoard();
        }
    }
}