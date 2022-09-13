using DEMAT.ApplicationServices.Jwt;
using DEMAT.ApplicationServices.Sendgrid;
using DEMAT.Models.Sendgrid;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using DEMAT.Infrastructure.Identity;
using DEMAT.Infrastructure.Identity.Models;
using DEMAT.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SendGrid.Helpers.Errors.Model;
using System.ComponentModel.DataAnnotations;
using DEMAT.Models.Dtos;

namespace DEMAT.ApplicationServices.Identity
{
    public class AuthUserService : IAuthUserService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailSender _emailSender;
        private readonly IDematContext _amenBankContext;
        private readonly AppIdentityDbContext _appIdentityContext;
        private IMediator _mediator;
        private readonly IAuthUserService _userService;
        private readonly IAmenUnitOfWork _amenUnitOfWork;


        public AuthUserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager
          , ITokenService tokenService, IEmailSender emailSender, IDematContext amenBankContext, AppIdentityDbContext appIdentityContext, IAmenUnitOfWork amenUnitOfWork, IMediator mediator)
        {
            _amenBankContext = amenBankContext;
            _amenUnitOfWork = amenUnitOfWork;
            _mediator = mediator;

            _userManager = userManager;
            _appIdentityContext = appIdentityContext;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailSender = emailSender;
        }
        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)  throw new Exception("Wrong Data");
            var LockoutEndDate = await _userManager.GetLockoutEndDateAsync(user);

            if (LockoutEndDate > DateTime.Now)
            {
                throw new Exception("Your account is locked out temporarily ,wait 1 minute");
               
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded) {
                await _userManager.AccessFailedAsync(user);
                throw new Exception("Wrong Data"); 
            }

            await _userManager.ResetAccessFailedCountAsync(user);
            return new AuthResponse { 
                Email = request.Email,
                DiplayName = user.DisplayName,
                Token = await _tokenService.CreateToken(user),
                Message="You are connected Successfully"
            };
        }

        public async Task<UserDto> GetCurrentUser(string email, string role)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user)
            };
        }

        public async Task<IdentityResult> ResetPassword(string email, string newPassword, string identityToken, string secondSecurityLayerToken)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new Exception("There is no user associated with this email");
            var validToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(identityToken));
            var result = await _userManager.ResetPasswordAsync(user, validToken, newPassword);
            Console.WriteLine(user.ResetToken);
            if (user.ResetToken != secondSecurityLayerToken) throw new Exception("Invalid Token");
            var nowDateInTimestamp = _tokenService.DateTimeToTimestamp(DateTime.Now);
            if (Int32.Parse(nowDateInTimestamp) > Int32.Parse(user.ExpirationDate)) throw new Exception("Token Expired");
            if (!result.Succeeded) return result;
            return result;

        }

        public async Task<bool> SendResetEmail(string email)
        {
            var randomHex = _tokenService.GetRandomHexNumber(10);
            var hash = _tokenService.ToSHA256(email);


            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new Exception("There is no user associated with this email");
            }


            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var validToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            user.ResetToken = randomHex + hash;
            user.ExpirationDate = _tokenService.DateTimeToTimestamp(DateTime.Now.AddHours(2));
            await _userManager.UpdateAsync(user);

            Console.WriteLine(randomHex + hash);

            var emailToSend = new Email
            {
                To = email,
                Body = "<!DOCTYPE html><html lang='en' xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:v='urn:schemas-microsoft-com:vml'> <head> <title></title> <meta content='text/html; charset=utf-8' http-equiv='Content-Type'/> <meta content='width=device-width,initial-scale=1' name='viewport'/><!--[if mso]><xml ><o:OfficeDocumentSettings ><o:PixelsPerInch>96</o:PixelsPerInch ><o:AllowPNG/></o:OfficeDocumentSettings></xml ><![endif]--> <style>*{box - sizing: border-box;}body{margin: 0; padding: 0;}a[x-apple-data-detectors]{color: inherit !important; text-decoration: inherit !important;}#MessageViewBody a{color: inherit; text-decoration: none;}p{line - height: inherit;}@media (max-width: 690px){.icons - inner{text-align: center;}.icons-inner td{margin: 0 auto;}.row-content{width: 100% !important;}.image_block img.big{width: auto !important;}.column .border{display: none;}table{table-layout: fixed !important;}.stack .column{width: 100%; display: block;}}</style> </head> <body style=' background-color: #37474f; margin: 0; padding: 0; -webkit-text-size-adjust: none; text-size-adjust: none; ' > <table border='0' cellpadding='0' cellspacing='0' class='nl-container' role='presentation' style=' mso-table-lspace: 0; mso-table-rspace: 0; background-color: #37474f; ' width='100%' > <tbody> <tr> <td> <table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-1' role='presentation' style='mso-table-lspace: 0; mso-table-rspace: 0' width='100%' > <tbody> <tr> <td> <table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style=' mso-table-lspace: 0; mso-table-rspace: 0; background-color: #b1e5db; color: #000; width: 670px; ' width='670' > <tbody> <tr> <td class='column column-1' style=' mso-table-lspace: 0; mso-table-rspace: 0; font-weight: 400; text-align: left; vertical-align: top; padding-top: 5px; padding-bottom: 5px; border-top: 0; border-right: 0; border-bottom: 0; border-left: 0; ' width='100%' > <table border='0' cellpadding='0' cellspacing='0' class='image_block' role='presentation' style='mso-table-lspace: 0; mso-table-rspace: 0' width='100%' > <tr> <td style=' width: 100%; padding-right: 0; padding-left: 0; ' > <div align='center' style='line-height: 10px'> <a href='www.example.com' style='outline: none' tabindex='-1' target='_blank' ><img alt='company logo' src='https://res.cloudinary.com/habibii/image/upload/v1652044039/logo_1_x7m5ri.png' style=' display: block; height: auto; border: 0; width: 116px; max-width: 100%; ' title='company logo' width='116'/></a> </div></td></tr></table> </td></tr></tbody> </table> </td></tr></tbody> </table> <table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-2' role='presentation' style='mso-table-lspace: 0; mso-table-rspace: 0' width='100%' > <tbody> <tr> <td> <table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style=' mso-table-lspace: 0; mso-table-rspace: 0; background-color: #b1e5db; color: #000; width: 670px; ' width='670' > <tbody> <tr> <td class='column column-1' style=' mso-table-lspace: 0; mso-table-rspace: 0; font-weight: 400; text-align: left; vertical-align: top; padding-top: 5px; padding-bottom: 5px; border-top: 0; border-right: 0; border-bottom: 0; border-left: 0; ' width='100%' > <table border='0' cellpadding='0' cellspacing='0' class='image_block' role='presentation' style='mso-table-lspace: 0; mso-table-rspace: 0' width='100%' > <tr> <td style=' width: 100%; padding-right: 0; padding-left: 0; ' > <div align='center' style='line-height: 10px'> <a href='www.example.com' style='outline: none' tabindex='-1' target='_blank' ><img alt='reset password' class='big' src='https://res.cloudinary.com/habibii/image/upload/v1652043889/3275432_oxuc72.png' style=' display: block; height: auto; border: 0; width: 670px; max-width: 100%; ' title='reset password' width='670'/></a> </div></td></tr></table> <table border='0' cellpadding='0' cellspacing='0' class='text_block' role='presentation' style=' mso-table-lspace: 0; mso-table-rspace: 0; word-break: break-word; ' width='100%' > <tr> <td style='padding-top: 40px'> <div style='font-family: Arial, sans-serif'> <div class='txtTinyMce-wrapper' style=' font-size: 14px; font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; mso-line-height-alt: 16.8px; color: #393d47; line-height: 1.2; ' >" +
                $"<p style=' margin: 0; text-align: center; font-size: 16px; ' > Bonjour <b>{user.FirstName}{user.LastName}</b> , </p><p style=' margin: 0; text-align: center; font-size: 16px; ' > nous vous envoyons cet e-mail car vous avez demandé la réinitialisation de votre mot de passe. Cliquez sur ce lien pour créer un nouveau mot de passe. </p></div></div></td></tr></table> <table border='0' cellpadding='20' cellspacing='0' class='button_block' role='presentation' style='mso-table-lspace: 0; mso-table-rspace: 0' width='100%' > <tr> <td> <div align='center'> <a href='https://demat.dev2.addinn-group.com/SinIn/reset-password?hex={randomHex}&token={validToken}' style=' text-decoration: none; display: inline-block; color: #d6f8f2; background-color: #37474f; border-radius: 24px; width: auto; border-top: 1px solid #37474f; font-weight: 400; border-right: 1px solid #37474f; border-bottom: 1px solid #37474f; border-left: 1px solid #37474f; padding-top: 5px; padding-bottom: 5px; font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all; ' target='_blank' ><span style=' padding-left: 15px; padding-right: 15px; font-size: 16px; display: inline-block; letter-spacing: 1px; ' ><span style=' font-size: 16px; line-height: 2; word-break: break-word; mso-line-height-alt: 32px; ' ><strong>Cliquer ici</strong></span ></span ></a > </div></td></tr></table> </td></tr></tbody> </table> </td></tr></tbody> </table> <table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-3' role='presentation' style='mso-table-lspace: 0; mso-table-rspace: 0' width='100%' > <tbody> <tr> <td> <table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style=' mso-table-lspace: 0; mso-table-rspace: 0; background-color: #1f1f20; color: #000; width: 670px; ' width='670' > <tbody> <tr> <td class='column column-1' style=' mso-table-lspace: 0; mso-table-rspace: 0; font-weight: 400; text-align: left; vertical-align: top; border-top: 0; border-right: 0; border-bottom: 0; border-left: 0; ' width='33.333333333333336%' > <table border='0' cellpadding='0' cellspacing='0' class='image_block' role='presentation' style='mso-table-lspace: 0; mso-table-rspace: 0' width='100%' > <tr> <td style=' padding-bottom: 50px; padding-left: 25px; padding-right: 25px; padding-top: 50px; width: 100%; ' > " +
                $"<div style='line-height: 10px'> <a href='www.example.com' style='outline: none' tabindex='-1' target='_blank' ><img alt='company logo' src='https://res.cloudinary.com/habibii/image/upload/v1652043965/HelloTech-qr-code_u3leqk.jpg' style=' display: block; height: auto; border: 0; width: 168px; max-width: 100%; ' title='company logo' width='168'/></a> </div></td></tr></table> </td><td class='column column-2' style=' mso-table-lspace: 0; mso-table-rspace: 0; font-weight: 400; text-align: left; vertical-align: top; border-top: 0; border-right: 0; border-bottom: 0; border-left: 0; ' width='33.333333333333336%' > <table border='0' cellpadding='0' cellspacing='0' class='heading_block' role='presentation' style='mso-table-lspace: 0; mso-table-rspace: 0' width='100%' > <tr> <td style=' padding-left: 20px; text-align: center; width: 100%; padding-top: 25px; ' > <h3 style=' margin: 0; color: #fff; direction: ltr; font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 16px; font-weight: 400; line-height: 200%; text-align: left; margin-top: 0; margin-bottom: 0; ' > <strong>A propos</strong> </h3> </td></tr></table> <table border='0' cellpadding='10' cellspacing='0' class='divider_block' role='presentation' style='mso-table-lspace: 0; mso-table-rspace: 0' width='100%' > <tr> <td> <table border='0' cellpadding='0' cellspacing='0' role='presentation' style=' mso-table-lspace: 0; mso-table-rspace: 0; ' width='80%' > <tr> <td class='divider_inner' style=' font-size: 1px; line-height: 1px; border-top: 2px solid #bbb; ' > <span> </span> </td></tr></table> </td></tr></table> <table border='0' cellpadding='0' cellspacing='0' class='text_block' role='presentation' style=' mso-table-lspace: 0; mso-table-rspace: 0; word-break: break-word; ' width='100%' > <tr> <td style=' padding-bottom: 15px; padding-left: 20px; padding-right: 20px; padding-top: 10px; ' > <div style='font-family: sans-serif'> <div class='txtTinyMce-wrapper' style=' font-size: 12px; font-family: Helvetica Neue, Helvetica, Arial, sans-serif; mso-line-height-alt: 18px; color: #fff; line-height: 1.5; ' > <p style=' margin: 0; font-size: 14px; mso-line-height-alt: 18px; ' > <span style='font-size: 12px' >Voluptate cillum occaecat officia laboris eu cillum enim sit cupidatat quis exercitation. Laborum ex commodo enim laborum laboris ipsum do amet aliqua proident ullamco eiusmod tempor dolor. .<br/></span> </p></div></div></td></tr></table> </td><td class='column column-3' style=' mso-table-lspace: 0; mso-table-rspace: 0; font-weight: 400; text-align: left; vertical-align: top; border-top: 0; border-right: 0; border-bottom: 0; border-left: 0; ' width='33.333333333333336%' > <table border='0' cellpadding='0' cellspacing='0' class='heading_block' role='presentation' style='mso-table-lspace: 0; mso-table-rspace: 0' width='100%' > <tr> <td style=' padding-left: 20px; text-align: center; width: 100%; padding-top: 25px; ' > <h3 style=' margin: 0; color: #fff; direction: ltr; font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 16px; font-weight: 400; line-height: 200%; text-align: left; margin-top: 0; margin-bottom: 0; ' > <strong>Contact</strong> </h3> </td></tr></table> <table border='0' cellpadding='10' cellspacing='0' class='divider_block' role='presentation' style='mso-table-lspace: 0; mso-table-rspace: 0' width='100%' > <tr> <td> <table border='0' cellpadding='0' cellspacing='0' role='presentation' style=' mso-table-lspace: 0; mso-table-rspace: 0; ' width='80%' > <tr> <td class='divider_inner' style=' font-size: 1px; line-height: 1px; border-top: 2px solid #bbb; ' > <span> </span> </td></tr></table> </td></tr></table> <table border='0' cellpadding='0' cellspacing='0' class='text_block' role='presentation' style=' mso-table-lspace: 0; mso-table-rspace: 0; word-break: break-word; ' width='100%' > <tr> <td style=' padding-bottom: 10px; padding-left: 20px; padding-right: 20px; padding-top: 10px; ' > <div style='font-family: sans-serif'> <div class='txtTinyMce-wrapper' style=' font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #a9a9a9; line-height: 1.2; font-family: Helvetica Neue, Helvetica, Arial, sans-serif; ' > <p style='margin: 0; font-size: 14px'> <a href='http://www.example.com' rel='noopener' style=' text-decoration: none; color: #e9e7e7; ' target='_blank' >example@example.com</a > </p></div></div></td></tr></table> <table border='0' cellpadding='0' cellspacing='0' class='text_block' role='presentation' style=' mso-table-lspace: 0; mso-table-rspace: 0; word-break: break-word; ' width='100%' > <tr> <td style=' padding-bottom: 10px; padding-left: 20px; padding-right: 20px; padding-top: 10px; ' > <div style='font-family: sans-serif'> <div class='txtTinyMce-wrapper' style=' font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #a9a9a9; line-height: 1.2; font-family: Helvetica Neue, Helvetica, Arial, sans-serif; ' > <p style='margin: 0; font-size: 14px'> <a href='http://www.example.com' rel='noopener' style=' text-decoration: none; color: #e9e7e7; ' target='_blank' >Our Location</a > </p></div></div></td></tr></table> <table border='0' cellpadding='0' cellspacing='0' class='text_block' role='presentation' style=' mso-table-lspace: 0; mso-table-rspace: 0; word-break: break-word; ' width='100%' > <tr> <td style=' padding-bottom: 10px; padding-left: 20px; padding-right: 20px; padding-top: 10px; ' > <div style='font-family: sans-serif'> <div class='txtTinyMce-wrapper' style=' font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #a9a9a9; line-height: 1.2; font-family: Helvetica Neue, Helvetica, Arial, sans-serif; ' > <p style='margin: 0; font-size: 14px'> <a href='http://www.example.com' rel='noopener' style=' text-decoration: underline; color: #e9e7e7; ' target='_blank' >Unsubscribe</a > </p></div></div></td></tr></table> <table border='0' cellpadding='0' cellspacing='0' class='social_block' role='presentation' style='mso-table-lspace: 0; mso-table-rspace: 0' width='100%' > <tr> <td style=' padding-bottom: 35px; padding-left: 20px; padding-right: 10px; padding-top: 10px; text-align: left; ' > <table align='left' border='0' cellpadding='0' cellspacing='0' class='social-table' role='presentation' style=' mso-table-lspace: 0; mso-table-rspace: 0; ' width='156px' > <tr> <td style='padding: 0 20px 0 0'> <a href='https://www.facebook.com/' target='_blank' ><img alt='Facebook' height='32' src='https://res.cloudinary.com/habibii/image/upload/v1652043926/facebook2x_ftfidp.png' style=' display: block; height: auto; border: 0; ' title='facebook' width='32'/></a> </td><td style='padding: 0 20px 0 0'> <a href='https://www.twitter.com/' target='_blank' ><img alt='Twitter' height='32' src='https://res.cloudinary.com/habibii/image/upload/v1652044063/twitter2x_aamqpt.png' style=' display: block; height: auto; border: 0; ' title='twitter' width='32'/></a> </td><td style='padding: 0 20px 0 0'> <a href='https://www.instagram.com/' target='_blank' ><img alt='Instagram' height='32' src='https://res.cloudinary.com/habibii/image/upload/v1652044003/instagram2x_ahwsid.png' style=' display: block; height: auto; border: 0; ' title='instagram' width='32'/></a> </td></tr></table> </td></tr></table> </td></tr></tbody> </table> </td></tr></tbody> </table> <table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-4' role='presentation' style='mso-table-lspace: 0; mso-table-rspace: 0' width='100%' > <tbody> <tr> <td> <table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style=' mso-table-lspace: 0; mso-table-rspace: 0; color: #000; width: 670px; ' width='670' > <tbody> <tr> <td class='column column-1' style=' mso-table-lspace: 0; mso-table-rspace: 0; font-weight: 400; text-align: left; vertical-align: top; padding-top: 5px; padding-bottom: 5px; border-top: 0; border-right: 0; border-bottom: 0; border-left: 0; ' width='100%' > <table border='0' cellpadding='0' cellspacing='0' class='icons_block' role='presentation' style='mso-table-lspace: 0; mso-table-rspace: 0' width='100%' > <tr> <td style=' vertical-align: middle; color: #9d9d9d; font-family: inherit; font-size: 15px; padding-bottom: 5px; padding-top: 5px; text-align: center; ' > <table cellpadding='0' cellspacing='0' role='presentation' style=' mso-table-lspace: 0; mso-table-rspace: 0; ' width='100%' > <tr> <td style=' vertical-align: middle; text-align: center; ' > <table cellpadding='0' cellspacing='0' class='icons-inner' role='presentation' style=' mso-table-lspace: 0; mso-table-rspace: 0; display: inline-block; margin-right: -4px; padding-left: 0; padding-right: 0; ' > <tr> <td style=' vertical-align: middle; text-align: center; padding-top: 5px; padding-bottom: 5px; padding-left: 5px; padding-right: 6px; ' > </td><td style=' font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 15px; color: #9d9d9d; vertical-align: middle; letter-spacing: undefined; text-align: center; ' > </td></tr></table> </td></tr></table> </td></tr></table> </td></tr></tbody> </table> </td></tr></tbody> </table> </td></tr></tbody> </table> </body></html>",
                Subject = "Réinitialisez votre DEMAT mot de passe"
            };

            //var response = true;
            var response = await _emailSender.SendEmail(emailToSend);
            return response;
        }
        public async Task<string> UpdatePassword(string id, string currentPWD, string newPWD, string confirmdPWD, CancellationToken cancellationToken)
        {
            // var user = await _userManager.FindByNameAsync(id);

            AppUser user = await _appIdentityContext.Users
                        .Where(x => x.UserName.Equals(id))
                        .FirstOrDefaultAsync(cancellationToken);

            if (user == null)

            {
                return "User does not found";
            }
            var hasher = new PasswordHasher<AppUser>();
            var resultPWD = hasher.VerifyHashedPassword(user, user.PasswordHash, currentPWD);
            if (resultPWD == PasswordVerificationResult.Failed)
                return "the current password is wrong";

            if (!newPWD.Equals(confirmdPWD))
                return "the new password and confirmed password is not match";


            var result = await _userManager.ChangePasswordAsync(user, currentPWD, newPWD);
            if (!result.Succeeded)
            {
                var errors = new List<string>();
                foreach (var error in result.Errors)
                { errors.Add(error.Description); }

                return " errors";
            }
            return "Password successfully changed";



        }
        public async Task<string> GetUserIdAsync(string userName, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                throw new NotFoundException("User not found");
                //throw new Exception("User not found");
            }
            return await _userManager.GetUserIdAsync(user);
        }

        public async Task<List<(string Id, string DisplayName, string FirstName, string LastName, string UserName, string Email, string PhoneNumber)>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.Select(x => new
            {
                x.Id,
                x.DisplayName,
                x.FirstName,
                x.LastName,
                x.UserName,
                x.Email,
                x.PhoneNumber
            }).ToListAsync();

            return users.Select(user => (user.Id, user.DisplayName,user.FirstName,user.LastName, user.UserName, user.Email , user.PhoneNumber)).ToList();
        }


        public async Task<(bool isSucceed, string Id)> CreateUserAsync(string DisplayName,string userName, string password, string email, string FirstName, string LastName, List<string> roles)
        {
            var user = new AppUser()
            {
                DisplayName = DisplayName,
                FirstName = FirstName,
                LastName = LastName,
                UserName = userName,
                Email = email
               
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
           //throw new ValidationException(result.Errors);
            }

            var addUserRole = await _userManager.AddToRolesAsync(user, roles);
            if (!addUserRole.Succeeded)
            {
                //throw new ValidationException(addUserRole.Errors);
            }
            return (result.Succeeded, user.Id);
        }


        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
                //throw new Exception("User not found");
            }

            if (user.UserName == "system" || user.UserName == "admin")
            {
                throw new Exception("You can not delete system or admin user");
                //throw new BadRequestException("You can not delete system or admin user");
            }
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }



        public async Task<bool> UpdateUserProfile(string id, string LastName,string FirstName, string email, IList<string> roles)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.LastName = LastName;
            user.FirstName = FirstName;
            user.Email = email;
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

    }
}
    
