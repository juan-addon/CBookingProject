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
    public class RoomPricesController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;

        public RoomPricesController(DataContext context, ICombosHelper combosHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
        }

        // GET: RoomTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.RoomPrices.Include(m=>m.RoomAvailabilities).ToListAsync());
        }

        // GET: RoomTypes/Create
        public IActionResult Create()
        {

            RoomPriceViewModel model = new RoomPriceViewModel
            {
                RoomAvailabilities = _combosHelper.GetAvailabilitiesList()
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomPrice roomPrice)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(roomPrice);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "The price already exists.");
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
            return View(roomPrice);
        }
        // GET: RoomTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RoomPrice roomPrice = await _context.RoomPrices
              .Include(x => x.RoomAvailabilities)
              .FirstOrDefaultAsync(x => x.RoomPriceId == id);

            if (roomPrice == null)
            {
                return NotFound();
            }

            RoomPriceViewModel model = _converterHelper.ToRoomPriceViewModel(roomPrice);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoomPrice roomPrice)
        {
            if (id != roomPrice.RoomPriceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    _context.Update(roomPrice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "The price already exists.");
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

            RoomPriceViewModel model = _converterHelper.ToRoomPriceViewModel(roomPrice);
            return View(model);
        }

        // GET: RoomTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RoomPrice roomPrice = await _context.RoomPrices
                .FirstOrDefaultAsync(m => m.RoomPriceId == id);
            if (roomPrice == null)
            {
                return NotFound();
            }

            _context.RoomPrices.Remove(roomPrice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
