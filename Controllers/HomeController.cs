using System.Linq;
using System.Web.Mvc;

namespace CRUD.Controllers
{
    // This is the HomeController class that handles CRUD operations on products
    public class HomeController : Controller
    {
        // This is the database context object that connects to the database
        MVCCRUDDBContext _context = new MVCCRUDDBContext();

        // This is the Index action that returns the view with the cheapest products
        public ActionResult Index()
        {
            // This is a LINQ query that orders the products by price and takes the first three
            var cheapestProducts = _context.Product.OrderBy(p => p.Price).Take(3).ToList();
            // Pass the products to the view
            return View(cheapestProducts);
        }

        // This method returns the view for creating a new product
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // This method handles the post request for creating a new product
        [HttpPost]
        public ActionResult Create(Product model)
        {
            // Add the product to the database
            _context.Product.Add(model);
            // Save the changes
            _context.SaveChanges();
            // Set a message to display on the view
            //ViewBag.Message = "Data was inserted successfully.";
            // Return the same view
            return View();
        }

        // This method returns the view for editing an existing product
        [HttpGet]
        public ActionResult Edit(int id)
        {
            // Find the product by id
            var data = _context.Product.Where(x => x.Id == id).FirstOrDefault();
            // Return the view with the product data
            return View(data);
        }

        // This method handles the post request for editing an existing product
        [HttpPost]
        public ActionResult Edit(Product Model)
        {
            // Find the product by id
            var data = _context.Product.Where(x => x.Id == Model.Id).FirstOrDefault();
            // Check if the product exists
            if (data != null)
            {
                // Update the product properties
                data.Name = Model.Name;
                data.Description = Model.Description;
                data.Quantity = Model.Quantity;
                data.Price = Model.Price;
                // Save the changes
                _context.SaveChanges();
            }
            // Redirect to the index action
            return RedirectToAction("index");
        }

        // This method returns the view for displaying the details of a product
        public ActionResult Details(int id)
        {
            // Find the product by id and check if exists
            var Product = _context.Product.Find(id);

            if (Product == null)
            {
                return HttpNotFound();
            }
            //var data = _context.Product.Where(x => x.Id == id).FirstOrDefault();
            return View(Product);
        }

        // This method handles the request for deleting a product
        public ActionResult Delete(int id)
        {
            var data = _context.Product.Where(x => x.Id == id).FirstOrDefault();
            // Remove the product from the database
            _context.Product.Remove(data);
            _context.SaveChanges();
            ViewBag.Message = "Record was deleted sucessfully";
            return RedirectToAction("index");
        }
        public ActionResult AllProducts()
        {
            var allProducts = _context.Product.ToList();
            return View(allProducts);
        }
    }
}