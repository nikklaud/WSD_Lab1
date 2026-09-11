using Microsoft.AspNetCore.Mvc;

namespace WSD_Lab1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private static readonly List<Course> courses = new()
    {
        new Course
        {
            Id = 1,
            Name = "Web Services",
            Teacher = "Teacher 1",
            Credits = 5
        },
        new Course
        {
            Id = 2,
            Name = "Databases",
            Teacher = "Teacher 2",
            Credits = 4
        }
    };
    
    [HttpGet]
    public ActionResult<List<Course>> GetAll()
    {
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public ActionResult<Course> GetById(int id)
    {
        var course = courses.FirstOrDefault(x => x.Id == id);

        if (course == null)
        {
            return NotFound();
        }

        return Ok(course);
    }
    
    [HttpPost]
    public ActionResult<Course> Create(Course course)
    {
        if (courses.Any(x => x.Id == course.Id))
        {
            return Conflict("Course with this ID already exists.");
        }

        courses.Add(course);

        return CreatedAtAction(
            nameof(GetById),
            new { id = course.Id },
            course
        );
    }

    [HttpPut("{id}")]
    public ActionResult<Course> Update(int id, Course updatedCourse)
    {
        var course = courses.FirstOrDefault(x => x.Id == id);

        if (course == null)
        {
            return NotFound();
        }

        course.Name = updatedCourse.Name;
        course.Teacher = updatedCourse.Teacher;
        course.Credits = updatedCourse.Credits;

        return Ok(course);
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var course = courses.FirstOrDefault(x => x.Id == id);

        if (course == null)
        {
            return NotFound();
        }

        courses.Remove(course);

        return NoContent();
    }
}