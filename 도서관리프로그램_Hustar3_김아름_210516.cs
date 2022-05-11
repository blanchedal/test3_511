using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarypj
{
    class Book
    {
        public void InputBookData()
        {
            Console.Write("도서 제목 : ");
            title_book = Console.ReadLine();
            Console.Write("ISBN 코드 : ");
            isbn_book = Console.ReadLine();
            inputauthor();
            inputlist();
            Console.Write("출판사 : ");
            publisher = Console.ReadLine();
        }
        public void inputauthor()
        {
            string enter = "^^";
            author_book.Clear();
            int i = 1;
            while (true)
            {
                Console.Write("저자 {0}: ", i);
                enter = Console.ReadLine();
                if (string.IsNullOrEmpty(enter))
                {
                    break;
                }
                author_book.Add(enter);
                i++;
            }
        }
        public void inputlist()
        {
            string enter = "^^";
            list_book.Clear();
            int i = 1;
            while (true)
            {
                Console.Write("목차 {0}: ", i);
                enter = Console.ReadLine();
                if (string.IsNullOrEmpty(enter))
                {
                    break;
                }
                list_book.Add(enter);
                i++;
            }
        }

        public void printBookData(int i)
        {            
            Console.WriteLine("==== 도서 {0} ====", i + 1);
            Console.WriteLine("도서 제목 : {0}", title_book);
            Console.WriteLine("ISBN 코드 : {0}", isbn_book);
            printauthor();
            printlist();
            Console.WriteLine("출판사 : {0}\n", publisher);
        }
        public void printauthor()
        {
            for (int i = 0; i < author_book.Count; i++) //저자 출력 for문
            {
                Console.WriteLine("저자 {0} : {1}", i + 1, author_book[i]);
            }
        }
        public void printlist()
        {
            for (int i = 0; i < list_book.Count; i++) //목차 출력 for문
            {
                Console.WriteLine("목차 {0} : {1}", i + 1, list_book[i]);
            }
        }

        public static bool CompareISBN(Book b, string str) // ISBN으로 검색
        {
            return b.isbn_book == str;
        }
        public static bool CompareTitle(Book b, string str) // 책 제목으로 검색
        {
            return b.title_book == str;
        }
        public static bool CompareAuthor(Book b, string str) // 저자 제목으로 검색
        {
            for (int i = 0; i < b.author_book.Count; i++)
            {
                if (str == b.author_book[i])
                {
                    return true;
                }
            }
            return false;
        }
        public static bool CompareList(Book b, string str) // 저자 제목으로 검색
        {
            for (int i = 0; i < b.list_book.Count; i++)
            {
                if (str == b.list_book[i])
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CompareFunc(string str, Book b, CompareDelegate comDelegate)
        {
            return comDelegate(b, str);
        }

        public void printauthorlist(string str,List<string> list)
        {
            for(int i = 0; i < list.Count; i++)
            {
                Console.WriteLine("{0} {1} : {2}", str, i + 1, list[i]);
            }
        }

        public void printOneBookData(int i)
        {
            Console.WriteLine("\n========== {0} =========", i);
            Console.WriteLine("도서 제목 : {0}", title_book);
            Console.WriteLine("ISBN 코드 : {0}", isbn_book);
            printauthorlist("저자", author_book);
            printauthorlist("목차", list_book);
            Console.WriteLine("출판사 : {0}", publisher);
        }

        private string title_book;
        private List<string> author_book = new List<string>();
        private List<string> list_book = new List<string>();
        private string isbn_book;
        private string publisher;

    }

    delegate bool CompareDelegate(Book b, string str);
    delegate void variableDelgate(string str, CompareDelegate b);

    class Library
    {
        public void inputBook()
        {
            Book b = new Book();
            b.InputBookData();
            newBookList.Add(b);
        }

        public void printBook()
        {
            for (int i = 0; i < newBookList.Count; i++)
            {
                Book b = newBookList[i];
                b.printBookData(i);
            }
        }

        public int SearchISBNFunc(string strTitle, CompareDelegate comDelegate)
        {
            Console.WriteLine(strTitle);
            string str = Console.ReadLine();
            for (int i = 0; i < newBookList.Count; i++)
            {
                if (comDelegate(newBookList[i], str))
                {
                    newBookList[i].printOneBookData(1);
                    return i;
                }
            }
            Console.WriteLine("찾으려는 데이터는 없습니다.");
            return -1;
        }

        public List<int> SearchlistFunc(string strTitle, CompareDelegate comDelegate)
        {
            List<int> n_searchList = new List<int>();
            Console.WriteLine(strTitle);
            string str = Console.ReadLine();
            int cnt = 1;
            for (int i = 0; i < newBookList.Count; i++)
            {
                if (comDelegate(newBookList[i], str))
                {
                    
                    newBookList[i].printOneBookData(cnt);
                    n_searchList.Add(i);
                    cnt++;
                }
            }
            if(n_searchList.Count == 0)
            {
                Console.WriteLine("찾으려는 데이터는 없습니다.");
            }
            return n_searchList;
        }

        public void searchBook()
        {
            Console.WriteLine("\n1. ISBN으로 검색");
            Console.WriteLine("2. 도서 제목으로 검색");
            Console.WriteLine("3. 저자로 검색");
            Console.WriteLine("4. 목차로 검색\n");
            Console.Write(">> ");
            int iChoice = int.Parse(Console.ReadLine());
            switch (iChoice)
            {
                case 1:
                    SearchISBNFunc("1. ISBN 코드", Book.CompareISBN);
                    break;
                case 2:
                    SearchlistFunc("2. 도서 제목", Book.CompareTitle);
                    break;
                case 3:
                    SearchlistFunc("3. 저자", Book.CompareAuthor);
                    break;
                case 4:
                    SearchlistFunc("4. 목차", Book.CompareList);
                    break;
            }

        }

        public void deldelegate(string str, CompareDelegate b)
        {
            List<int> n_searchList  = SearchlistFunc(str,b);
            if (n_searchList.Count != 0)
            {
                Console.WriteLine("\n삭제하려는 번호를 입력하시오. \n>> ");
                int iChoice = int.Parse(Console.ReadLine());
                newBookList.RemoveAt(n_searchList[iChoice - 1]);
            }
        }
       
        public void changedelegate(string str, CompareDelegate b)
        {
            List<int> n_searchList = SearchlistFunc(str, b);
            if (n_searchList.Count != 0)
            {
                Console.WriteLine("\n수정하려는 번호를 입력하시오. \n>> ");
                int iChoice = int.Parse(Console.ReadLine());
                newBookList[n_searchList[iChoice - 1]].InputBookData();
            }
        }

        public void choicesentence(string str)
        {
            Console.WriteLine("\n1. 도서 제목으로 검색 후{0}",str);
            Console.WriteLine("2. 저자로 검색 후{0}", str);
            Console.WriteLine("3. 목차로 검색 후{0}\n", str);
            Console.Write(">> ");
        }
        
        public void changeBook(string str, variableDelgate changedelegate)
        {
            choicesentence(str);
            int iChoice = int.Parse(Console.ReadLine());
            switch (iChoice)
            {
                case 1:
                    changedelegate("1. 도서 제목", Book.CompareTitle);
                    break;
                case 2:
                    changedelegate("2. 저자", Book.CompareAuthor);
                    break;
                case 3:
                    changedelegate("3. 목차", Book.CompareList);
                    break;
            }
        }
        public void Menu()
        {
            int ichoice = 1;
            while (ichoice != 0)
            {
                Console.WriteLine("\n=========================");
                Console.WriteLine("     1. 도서 등록");
                Console.WriteLine("     2. 도서 출력");
                Console.WriteLine("     3. 도서 검색");
                Console.WriteLine("     4. 도서 삭제");
                Console.WriteLine("     5. 도서 수정");
                Console.WriteLine("=========================\n");
                Console.Write(">> ");

                ichoice = int.Parse(Console.ReadLine());
                switch (ichoice)
                {
                    case 1:
                        inputBook();
                        break;
                    case 2:
                        printBook();
                        break;
                    case 3:
                        searchBook();
                        break;
                    case 4:
                        changeBook(" 삭제",deldelegate);
                        break;
                    case 5:
                        changeBook(" 수정",changedelegate);
                        break;
                    default:
                        break;
                }
            }
        }
        private List<Book> newBookList = new List<Book>();
    }
    class Program
    {
        static void Main(string[] args)
        {
            Library a = new Library();
            a.Menu();
        }
    }
}


