using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TestTask.BusinessLogicLayer.DTOs;
using TestTask.BusinessLogicLayer.Exceptions;
using TestTask.BusinessLogicLayer.Interfaces;
using TestTask.PresentationLayer.DTOs;

namespace TestTask.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        readonly ICategoryService categoryService;
        
        public CategoriesController(ICategoryService categoryService) 
        {
            this.categoryService = categoryService;
        }

        /// <summary>
        /// Получение всех категорий продуктов
        /// </summary>
        /// <remarks>
        /// Примерный вид результата:
        /// 
        ///     [
        ///         {
        ///             "id": 1,
        ///             "name": "Категория 1"
        ///         },
        ///         {
        ///             "id": 2,
        ///             "name": "Категория 2"
        ///         },
        ///         {
        ///             "id": 3,
        ///             "name": "Категория 3"
        ///         }
        ///     ]
        /// 
        /// </remarks>
        /// <response code="200">Возвращает результат запроса</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await categoryService.GetCategories());
        }

        /// <summary>
        /// Создание новой категории
        /// </summary>
        /// <param name="dto">Информация о создаваемой категории</param>
        /// <response code="200">Категория создана</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCategory(CategoryEditDTO dto)
        {
            await categoryService.CreateCategory(dto);
            return Ok();
        }

        /// <summary>
        /// Обновление категории с заданым ID
        /// </summary>
        /// <param name="id">ID категории</param>
        /// <param name="dto">Новая информация о категории</param>
        /// <response code="200">Категория обновлена</response>
        /// <response code="404">Категория не найдена</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody]CategoryEditDTO dto)
        {
            try
            {
                await categoryService.UpdateCategory(id, dto);
                return Ok();
            }
            catch (UnknownEntityException ex)
            {
                return NotFound(new ErrorDTO { Error = ex.Message});
            }
        }

        /// <summary>
        /// Удаление категории с заданым ID
        /// </summary>
        /// <param name="id">ID категории</param>
        /// <response code="200">Категория удалена</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await categoryService.DeleteCategory(id);
            return Ok();
        }

    }
}
