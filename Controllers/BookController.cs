using Microsoft.AspNetCore.Mvc;
using Mysqlx;
using SiemensBookAPI.Data;
using SiemensBookAPI.Models;

namespace SiemensBookAPI.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class BookController : ControllerBase
{
        DataContextDapper _dapper;
        public BookController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }

        [HttpPost("PopulateDB")]
        public IActionResult PopulateDB()
        {
            string sql = @"DROP TABLE IF EXISTS book;
            CREATE TABLE book (
                bid INT NOT NULL AUTO_INCREMENT,
                title VARCHAR(30),
                author VARCHAR(30),
                publisher VARCHAR(30),
                numberTotal INT,
                numberGiven INT,
                PRIMARY KEY (bid)
            );
            INSERT INTO book (title, author, publisher, numberTotal, numberGiven) VALUES
            ('The Great Gatsby', 'F. Scott Fitzgerald', 'Scribner', 5, 2),
            ('To Kill a Mockingbird', 'Harper Lee', 'J.B. Lippincott & Co.', 3, 1),
            ('1984', 'George Orwell', 'Secker & Warburg', 4, 0),
            ('Pride and Prejudice', 'Jane Austen', 'T. Egerton', 2, 1),
            ('The Catcher in the Rye', 'J.D. Salinger', 'Little, Brown and Company', 6, 3);";
            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to create table!");
        }

        [HttpGet("getbooks")]
        public IEnumerable<Book> GetPostari()
        {
            return _dapper.LoadData<Book>("SELECT * FROM book");
        }
        
        [HttpGet("getbook/{id}")]
        public Book GetPostare(int id)
        {
            string sql="SELECT * FROM book WHERE bid ="+id;
            return _dapper.LoadDataSingle<Book>(sql);
        }
        [HttpPost("addbook")]
        public IActionResult AddPostare([FromBody] Book book)
        {
            string sql = $"INSERT INTO book (title, author, publisher, numberGiven, numberTotal) VALUES ('{book.Title}', '{book.Author}', '{book.Publisher}', 0, '{book.NumberTotal}')";
            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to add book!");
        }
        [HttpPut("editbook/{id}")]
        public IActionResult UpdatePostare(int id, [FromBody] Book book)
        {
            string sql = $"UPDATE book SET title='{book.Title}', author='{book.Author}', publisher='{book.Publisher}', numberGiven='{book.NumberGiven}', numberTotal='{book.NumberTotal}' WHERE bid={id}";
            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to update book!");
        }
        [HttpDelete("deletebook/{id}")]
        public IActionResult DeletePostare(int id)
        {
            string sql = $"DELETE FROM book WHERE bid={id}";
            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to delete book!");
        }
        [HttpGet("getbookbytitle/{title}")]
        public Book GetPostareByTitle(string title)
        {
            string sql = $"SELECT * FROM book WHERE title='{title}'";
            return _dapper.LoadDataSingle<Book>(sql);
        }
        [HttpGet("getbookbyauthor/{author}")]
        public Book GetPostareByAuthor(string author)
        {
            string sql = $"SELECT * FROM book WHERE author='{author}'";
            return _dapper.LoadDataSingle<Book>(sql);
        }
        [HttpPut("givebook/{id}")]
        public IActionResult GiveBook(int id)
        {
            string sql = $"UPDATE book SET numberGiven=numberGiven+1 WHERE bid={id} AND numberGiven<numberTotal";
            if (_dapper.ExecuteSqlWithRowCount(sql) != 0)
            {
                 return Ok();
            }
            throw new Exception("Failed to give book!");
        }
        [HttpPut("returnbook/{id}")]
        public IActionResult ReturnBook(int id)
        {
            string sql = $"UPDATE book SET numberGiven=numberGiven-1 WHERE bid={id} AND numberGiven>0";
            if (_dapper.ExecuteSqlWithRowCount(sql) != 0)
            {
                return Ok();
            }
            throw new Exception("Failed to return book!");
        }
}