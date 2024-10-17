using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopBusinessLayer.ApplicationConstants;
using ShopBusinessLayer.Common;
using ShopBusinessLayer.InputModels;
using ShopBusinessLayer.Services;
using ShopBusinessLayer.Services.Interfaces;
using System.Net;

namespace MegaShopWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected APIResponse _response;

        public UserController(IAuthService authService)
        {
                _authService = authService;
               _response = new APIResponse();
        }

        [HttpPost,Route("Register")]
        public async Task<ActionResult<APIResponse>> Register(Register register)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.AddError(ModelState.ToString());
                    _response.AddWarning(CommonMessage.RegistrationFailed);
                    return _response;
                }

                var result = await _authService.Register(register);

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;
                _response.DisplayMessage = CommonMessage.RegistrationSuccess;
                _response.Result = result;

            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessage.SystemError);
            }

            return _response;
        }

        [HttpPost, Route("Login")]
        public async Task<ActionResult<APIResponse>> Login(Login login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.AddError(ModelState.ToString());
                    _response.AddWarning(CommonMessage.RegistrationFailed);
                    return _response;
                }

                var result = await _authService.Login(login);
                if (result is string)
                {
                    _response.StatusCode=HttpStatusCode.BadRequest;
                    _response.DisplayMessage=CommonMessage.LoginFailed;
                    _response.Result = result;
                    return _response;
                }

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.DisplayMessage = CommonMessage.LoginSuccess;
                _response.Result = result;

            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessage.SystemError);
            }

            return _response;
        }
        [HttpPost, Route("ResetPassword")]
        public async Task<ActionResult<APIResponse>> ResetPassword([FromBody] ResetPassWord model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.ResetPasswordAsync(model);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }
    }
}
