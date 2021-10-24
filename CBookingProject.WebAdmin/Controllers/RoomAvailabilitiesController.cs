using CBookingProject.Data.Entities;
using CBookingProject.Data;
using CBookingProject.WebAdmin.Helpers;
using CBookingProject.WebAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CBookingProject.WebAdmin.Controllers
{
    public class RoomAvailabilitiesController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        public RoomAvailabilitiesController(DataContext context, ICombosHelper combosHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
        }
        // GET: Room
        public async Task<IActionResult> Index()
        {
            return View(await _context.RoomAvailabilities.ToListAsync());
        }

        // GET: Room/Create
        public IActionResult Create()
        {

            RoomAvailabilityViewModel model = new RoomAvailabilityViewModel
            {
                RoomTypes = _combosHelper.GetRoomTypes()
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomAvailability availability)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    DateTime endDate = availability.DateTo.AddDays(1).AddSeconds(-1);
                    availability.DateTo = endDate;
                    _context.Add(availability);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe este tipo de habitacion");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(availability);
        }

        // GET: RoomTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RoomAvailability availability = await _context.RoomAvailabilities.Include(x => x.RoomType)
              .FirstOrDefaultAsync(x => x.AvailabilityId == id);

            if (availability == null)
            {
                return NotFound();
            }

            RoomAvailabilityViewModel model = _converterHelper.ToRoomAvailabilityViewModel(availability);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoomAvailability availability)
        {
            if (id != availability.AvailabilityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    DateTime endDate = availability.DateTo.AddDays(1).AddSeconds(-1);
                    availability.DateTo = endDate;

                    _context.Update(availability);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe este tipo de habitacion");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                return RedirectToAction(nameof(Index));
            }

            RoomAvailabilityViewModel model = _converterHelper.ToRoomAvailabilityViewModel(availability);
            return View(model);
        }

        // GET: RoomTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RoomAvailability availability = await _context.RoomAvailabilities
                .FirstOrDefaultAsync(m => m.AvailabilityId == id);
            if (availability == null)
            {
                return NotFound();
            }

            _context.RoomAvailabilities.Remove(availability);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
