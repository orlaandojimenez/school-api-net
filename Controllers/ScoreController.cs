using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ScoreController : ControllerBase
    {
        private readonly MySqlConnection _dbConnection;

        public ScoreController(MySqlConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpPost]
        public async Task<IActionResult> CreateScore([FromBody] ScoreRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request.");
            }

            try
            {
               await _dbConnection.OpenAsync();

                using var command = new MySqlCommand("insert_score", _dbConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("p_student_id", request.StudentId);
                command.Parameters.AddWithValue("p_grade_id", request.GradeId);
                command.Parameters.AddWithValue("p_subject_id", request.SubjectId);
                command.Parameters.AddWithValue("p_month", request.Month);
                command.Parameters.AddWithValue("p_score", request.Score);

                var result = await command.ExecuteNonQueryAsync();

                if (result > 0)
                {
                    return Ok("Score created successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to create score.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
            finally
            {
                await _dbConnection.CloseAsync();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetScoresByStudent(
        [FromQuery] int studentId, 
        [FromQuery] int? gradeId, 
        [FromQuery] int? month)
        {
            if (studentId <= 0)
            {
                return BadRequest("Invalid student ID.");
            }

            try
            {
                await _dbConnection.OpenAsync();

                using var command = new MySqlCommand("get_scores_by_student", _dbConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("p_student_id", studentId);
                command.Parameters.AddWithValue("p_grade_id", gradeId);
                command.Parameters.AddWithValue("p_month", month);

                using var reader = await command.ExecuteReaderAsync();
                var scores = new List<object>();

                while (await reader.ReadAsync())
                {
                    scores.Add(new
                    {
                        ScoreId = reader.GetInt32(reader.GetOrdinal("score_id")),
                        StudentId = reader.GetInt32(reader.GetOrdinal("student_id")),
                        GradeName = reader.GetString(reader.GetOrdinal("grade_name")),
                        SubjectName = reader.GetString(reader.GetOrdinal("subject_name")),
                        Month = reader.GetInt32(reader.GetOrdinal("month")),
                        Score = reader.GetDecimal(reader.GetOrdinal("score"))
                    });
                }

                return Ok(scores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
            finally
            {
                await _dbConnection.CloseAsync();
            }
        }
    }

    public class ScoreRequest
    {
        public int StudentId { get; set; }
        public int GradeId { get; set; }
        public int SubjectId { get; set; }
        public int Month { get; set; }
        public decimal Score { get; set; }
    }
}