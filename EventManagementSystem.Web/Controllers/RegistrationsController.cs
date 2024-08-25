using EventManagementSystem.Core.Entities;
using EventManagementSystem.Core.Interfaces;
using EventManagementSystem.Web.Services;
using EventManagementSystem.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EventManagementSystem.Web.Controllers
{
    [Authorize]
    public class RegistrationsController : Controller
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IEventRepository _eventRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PdfService _pdfService;


        public async Task<IActionResult> DownloadTicket(int id)
        {
            var registration = await _registrationRepository.GetRegistrationByIdAsync(id);
            if (registration == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            if (registration.UserId != userId)
            {
                return Forbid();
            }

            var pdfBytes = _pdfService.GenerateTicket(registration);
            return File(pdfBytes, "application/pdf", $"Ticket_{registration.Event.Name}.pdf");
        }


        public RegistrationsController(IRegistrationRepository registrationRepository, IEventRepository eventRepository, UserManager<ApplicationUser> userManager, PdfService pdfService)
        {
            _registrationRepository = registrationRepository;
            _eventRepository = eventRepository;
            _userManager = userManager;
            _pdfService = pdfService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var registrations = await _registrationRepository.GetUserRegistrationsAsync(userId);
            return View(registrations);
        }

        public async Task<IActionResult> Register(int eventId)
        {
            var @event = await _eventRepository.GetEventByIdAsync(eventId);
            if (@event == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var isRegistered = await _registrationRepository.IsUserRegisteredForEventAsync(userId, eventId);

            if (isRegistered)
            {
                TempData["ErrorMessage"] = "You are already registered for this event.";
                return RedirectToAction("Details", "Events", new { id = eventId });
            }

            var model = new RegistrationViewModel
            {
                EventId = eventId,
                EventName = @event.Name,
                EventDate = @event.DateAndTime
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var registration = new Registration
                {
                    EventId = model.EventId,
                    UserId = userId,
                    RegistrationDate = DateTime.Now
                };

                await _registrationRepository.AddRegistrationAsync(registration);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Cancel(int id)
        {
            var registration = await _registrationRepository.GetRegistrationByIdAsync(id);
            if (registration == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            if (registration.UserId != userId)
            {
                return Forbid();
            }

            return View(registration);
        }

        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelConfirmed(int id)
        {
            var registration = await _registrationRepository.GetRegistrationByIdAsync(id);
            if (registration == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            if (registration.UserId != userId)
            {
                return Forbid();
            }

            await _registrationRepository.DeleteRegistrationAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}