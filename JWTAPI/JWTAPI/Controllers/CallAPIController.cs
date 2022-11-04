using JWTAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JWTAPI.Controllers
{
    public class CallAPIController : Controller
    {
        public IActionResult Index(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            if ((username != "mani") || (password != "mani"))
                return View((object)"Login Failed");

            var accessToken = GenerateJSONWebToken();
            SetJWTCookie(accessToken);

            return RedirectToAction("TrainReservations");
        }

        private string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MynameisManikandan123"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "Equiniti",
                audience: "Equiniti",
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void SetJWTCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMinutes(2),
            };
            Response.Cookies.Append("jwtCookie", token, cookieOptions);
        }

        public async Task<IActionResult> TrainReservations()
        {
            var jwt = Request.Cookies["jwtCookie"];

            List<TrainReservation> reservationList = new List<TrainReservation>();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                using (var response = await httpClient.GetAsync("http://localhost:29844/Reservation")) 
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        reservationList = JsonConvert.DeserializeObject<List<TrainReservation>>(apiResponse);
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Index", new { message = "Please Login again" });
                    }
                }
            }

            return View(reservationList);
        }
    }
}
