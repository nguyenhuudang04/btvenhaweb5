﻿using Microsoft.AspNetCore.Mvc;
using WebApplication18.Models;
using WebApplication18.Repositories;

public class CategoriesController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    public CategoriesController(IProductRepository productRepository,
    ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }
    public async Task<IActionResult> Index()
    {
        var category = await _categoryRepository.GetAllAsync();
        return View(category);
    }
    public async Task<IActionResult> Display(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }
    public IActionResult Add()
    {
        return View();
    }

    // Action xử lý form tạo mới category
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(Category category)
    {
        if (ModelState.IsValid)
        {
            // Thêm danh mục vào database
            await _categoryRepository.AddAsync(category);

            // Trả về danh mục vừa tạo với trạng thái OK
            return RedirectToAction("Index", "Categories");
        }

        // Nếu ModelState không hợp lệ, trả lại form với lỗi
        return View(category);
    }

    public async Task<IActionResult> Update(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }
    [HttpPost]
    public async Task<IActionResult> Update(int id, Category category)
    {
        if (id != category.Id)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            _categoryRepository.UpdateAsync(category);
            return RedirectToAction(nameof(Index));

        }
        return View(category);
    }
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }
    [HttpPost, ActionName("DeleteConfirmed")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category != null)
        {
            _categoryRepository.DeleteAsync(id);
        }
        return RedirectToAction(nameof(Index));
    }
}