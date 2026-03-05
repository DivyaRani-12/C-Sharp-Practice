using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercise6
{
    class Book
    {
        public string ISBN { get;set;}
        public string Title {get;set;}

        public string Author {get;set;}
        public bool IsAvailable {get;set;}=true;
        public DateTime BorrowDate {get;set;}

        public Book(string isbn,string title,string author)
        {
            ISBN=isbn;
            Title=title;
            Author=author;
        }

        public override string ToString()
        {
            return $"{Title} by {Author} (ISBN:{ISBN}) - {(IsAvailable ? "Avilable":"Borrowed")}";

        }
    }
    class Member
    {
        public string MemberId {get;set;}
        public string Name {get;set;}

        public List<Book> BorroweBooks {get; set;} = new List<Book>();

        public Member(string id,string name)
        {
            MemberId=id;
            Name=name;
        }

        public bool CanBorrow()
        {
            return BorroweBooks.Count < 3;
        }
    }

    class Library
    {
        private List<Book> books = new List<Book>();
        private List<Member> members = new List<Member>();

        public void AddBook()
        {
            Console.Write("Enter ISBN:");
            string isbn = Console.ReadLine();

            Console.Write("Enter Title:");
            string title = Console.ReadLine();

            Console.Write("Enter Author:");
            string author = Console.ReadLine();

            books.Add(new Book(isbn,title,author));

            System.Console.WriteLine("Book added sucessfully");

        }

        public void RemoveBook()
        {
            Console.Write("Enter isbn to remove");
            string isbn = Console.ReadLine();

            var book = books.FirstOrDefault(b=>b.ISBN == isbn);

            if (book != null)
            {
                books.Remove(book);
                System.Console.WriteLine("Book removed. \n");
            }
            else
            {
                System.Console.WriteLine("Book not found.\n");
            }
        }

        public void RegisterMember()
        {
            Console.Write("Enter Member ID:");
            string id = Console.ReadLine();

            Console.Write("Enter Member Name:");
            string name=Console.ReadLine();

            members.Add(new Member(id,name));

            System.Console.WriteLine("Member registered sucessfully");
        }

        public void BorroweBooks()
        {
            Console.Write("Enter Member ID:");
            string MemberId = Console.ReadLine();

            Console.Write("Enter ISBN:");
            string isbn = Console.ReadLine();

            var member = members.FirstOrDefault(m=>m.MemberId == MemberId);
            var book = books.FirstOrDefault(b=>b.ISBN==isbn);

            if(member==null || book == null)
            {
                System.Console.WriteLine("Member or book not found");;
                return;
            }
            if (!book.IsAvailable)
            {
                System.Console.WriteLine("Book already borrowed.");
                return;
            }

            if (!member.CanBorrow())
            {
                System.Console.WriteLine("Member canot borrow more than 3 books.\n");
                return;
            }

            book.IsAvailable=false;
            book.BorrowDate = DateTime.Now;

            member.BorroweBooks.Add(book);

            System.Console.WriteLine("Book borrowed successfully.\n");
        }

        public void ReturnBook()
        {
            Console.Write("Enter Member ID:");
            string MemberId = Console.ReadLine();

            Console.Write("Enter Book ISBN:");
            string isbn = Console.ReadLine();

            var member=members.FirstOrDefault(m=>m.MemberId == MemberId);

            if(member == null)
            {
                System.Console.WriteLine("Member not found.\n");
                return;
            }

            var book=member.BorroweBooks.FirstOrDefault(b=>b.ISBN==isbn);

            if (book == null)
            {
                System.Console.WriteLine("This members didn't borrow that book.\n");
                return;
            }

            member.BorroweBooks.Remove(book);
            book.IsAvailable=true;

            decimal fee=CalculateLateFee(book);

            System.Console.WriteLine($"Book returned sucessfully. Late Fee:{fee}\n");

        }

        public decimal  CalculateLateFee(Book book)
        {
            int days = (DateTime.Now-book.BorrowDate).Days;

            if(days<=14)
                return 0;

            return (days-14)*2;
        }

        public void SearchByTitle()
        {
            Console.Write("Enter Title:");
            string title = Console.ReadLine();

            var results = books.Where(b=>
                b.Title.Contains(title,StringComparison.OrdinalIgnoreCase));

            foreach(var book in results)
                System.Console.WriteLine(book);

            System.Console.WriteLine();
        }

        public void SearchByAuthor()
        {
            Console.Write("Enter author:");
            string author=Console.ReadLine();

            var results = books.Where(b=>
                b.Author.Contains(author,StringComparison.OrdinalIgnoreCase));

            foreach(var book in results)
                System.Console.WriteLine(book);

            System.Console.WriteLine();
        }

        public void CheckAvailability()
        {
            Console.Write("Enter ISBN");
            string  isbn = Console.ReadLine();

            var book = books.FirstOrDefault(b=>b.ISBN==isbn);

            if(book==null)
                System.Console.WriteLine("Book not found");
            else
                System.Console.WriteLine(book.IsAvailable ? "Available\n":"Borrowed\n");
        }

        public void ListBorrowedBooks()
        {
            Console.Write("Enter member ID:");
            string MemberId=Console.ReadLine();

            var member = members.FirstOrDefault(m=>m.MemberId==MemberId);

            if(member==null)
            {
                System.Console.WriteLine("Member not found.");
                return;
            }

            foreach(var book in member.BorroweBooks)
                System.Console.WriteLine(book);

            System.Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            while (true)
            {
                System.Console.WriteLine("=====LIBRARY SYSTEM =====");
                System.Console.WriteLine("1. Add Book");
                System.Console.WriteLine("2. Remove Book");
                System.Console.WriteLine("3. Register member");
                System.Console.WriteLine("4. Borrow Book");
                System.Console.WriteLine("5. Return Book");
                System.Console.WriteLine("6. Search by Title");
                System.Console.WriteLine("7. Search by Author");
                System.Console.WriteLine("8. Chcek book Availability");
                System.Console.WriteLine("9. List Borrowed Books");
                System.Console.WriteLine("0. Exist");

                System.Console.WriteLine("Choose option:");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        library.AddBook();
                        break;

                    case 2:
                        library.RemoveBook();
                        break;

                    case 3:
                        library.RegisterMember();
                        break;

                    case 4:
                        library.BorroweBooks();
                        break;

                    case 5:
                        library.ReturnBook();
                        break;

                    case 6:
                        library.SearchByTitle();
                        break;

                    case 7:
                        library.SearchByAuthor();
                        break;

                    case 8:
                        library.CheckAvailability();
                        break;

                    case 9:
                        library.ListBorrowedBooks();
                        break;
                    
                    case 0:
                        return;

                    default:
                        System.Console.WriteLine("Invalid Choice\n");
                        break;

                }
            }
        }
    }
}