using IM_TLAB3.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IM_TLAB3.Contollers
{
	public class HomeController : Controller
	{
		public async Task<IActionResult> Index()
		{
			List<Product> products = new List<Product>();

			using (var httpClient = new HttpClient())
			{
				using (var responseP = await httpClient.GetAsync("http://localhost:7000/api/product"))
				{
					string apiResP = await responseP.Content.ReadAsStringAsync();
					products = JsonConvert.DeserializeObject<List<Product>>(apiResP);
				}
			}
			return View(products);
		}

		public async Task<IActionResult> IndexCategory()
		{
			List<Category> categories = new List<Category>();

			using (var httpClient = new HttpClient())
			{
				using (var responseC = await httpClient.GetAsync("http://localhost:7000/api/category"))
				{
					string apiResC = await responseC.Content.ReadAsStringAsync();
					categories = JsonConvert.DeserializeObject<List<Category>>(apiResC);
				}
			}
			return View(categories);
		}

		public IActionResult GetProduct() => View();

		public IActionResult GetCategory() => View();

		[HttpPost]
		public async Task<IActionResult> GetCategory(int Cid)
		{
			Category category = new Category();
			using (var httpClient = new HttpClient())
			{
				using (var response = await httpClient.GetAsync("http://localhost:7000/api/category/" + Cid))
				{
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						string apiRes = await response.Content.ReadAsStringAsync();
						category = JsonConvert.DeserializeObject<Category>(apiRes);
					}
					else
					{
						ViewBag.StatusCode = response.StatusCode;
					}
				}
			}
			return View(category);
		}


		[HttpPost]
		public async Task<IActionResult> GetProduct(int id)
		{
			Product product = new Product();
			using (var httpClient = new HttpClient())
			{
				using (var response = await httpClient.GetAsync("http://localhost:7000/api/product/" + id))
				{
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						string apiRes = await response.Content.ReadAsStringAsync();
						product = JsonConvert.DeserializeObject<Product>(apiRes);
					}
					else
					{
						ViewBag.StatusCode = response.StatusCode;
					}
				}
			}
			return View(product);
		}



		public IActionResult AddCategory() => View();

		[HttpPost]
		public async Task<IActionResult> AddCategory(Category category)
		{
			Category receiveCategory = new Category();
			using (var httpClient = new HttpClient())
			{
				StringContent content = new StringContent(JsonConvert.SerializeObject(category),
					Encoding.UTF8, "application/json");
				using (var response = await httpClient.PostAsync("http://localhost:7000/api/category", content))
				{
					string apiRes = await response.Content.ReadAsStringAsync();
					receiveCategory = JsonConvert.DeserializeObject<Category>(apiRes);
				}
			}
			return View(receiveCategory);
		}

		public IActionResult AddProduct() => View();
		[HttpPost]
		public async Task<IActionResult> AddProduct(Product product)
		{
			Product receiveProduct = new Product();
			using (var httpClient = new HttpClient())
			{
				StringContent content = new StringContent(JsonConvert.SerializeObject(product),
					Encoding.UTF8, "application/json");
				using (var response = await httpClient.PostAsync("http://localhost:7000/api/product", content))
				{
					string apiRes = await response.Content.ReadAsStringAsync();
					receiveProduct = JsonConvert.DeserializeObject<Product>(apiRes);
				}
			}
			return View(receiveProduct);
		}

		public async Task<IActionResult> UpdateProduct(int id)
		{
			Product product = new Product();
			using (var httpClient = new HttpClient())
			{
				using (var response = await httpClient.GetAsync("http://localhost:7000/api/product/" + id))
				{
					string apiRes = await response.Content.ReadAsStringAsync();
					product = JsonConvert.DeserializeObject<Product>(apiRes);
				}
			}
			return View(product);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateProduct(Product product)
		{
			Product getProduct = new Product();
			using (var httpClient = new HttpClient())
			{
				StringContent content = new StringContent(JsonConvert.SerializeObject(product),
					Encoding.UTF8, "application/json");

				using (var response = await httpClient.PutAsync("http://localhost:7000/api/product", content))
				{
					string apiRes = await response.Content.ReadAsStringAsync();
					ViewBag.Result = "Success";
					getProduct = JsonConvert.DeserializeObject<Product>(apiRes);
				}
			}
			return View(getProduct);
		}

		public async Task<IActionResult> UpdateCategory(int id)
		{
			Category category = new Category();
			using (var httpClient = new HttpClient())
			{
				using (var response = await httpClient.GetAsync("http://localhost:7000/api/category/" + id))
				{
					string apiRes = await response.Content.ReadAsStringAsync();
					category = JsonConvert.DeserializeObject<Category>(apiRes);
				}
			}
			return View(category);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateCategory(Category category)
		{
			Category getCategory = new Category();
			using (var httpClient = new HttpClient())
			{
				StringContent content = new StringContent(JsonConvert.SerializeObject(category),
					Encoding.UTF8, "application/json");

				using (var response = await httpClient.PutAsync("http://localhost:7000/api/category", content))
				{
					string apiRes = await response.Content.ReadAsStringAsync();
					ViewBag.Result = "Success";
					getCategory = JsonConvert.DeserializeObject<Category>(apiRes);
				}
			}
			return View(getCategory);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteProduct(int ProductId)
		{
			using (var httpClient = new HttpClient())
			{
				using (var response = await httpClient.DeleteAsync("http://localhost:7000/api/product/" + ProductId))
				{
					string apiRes = await response.Content.ReadAsStringAsync();
				}
			}

			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> DeleteCategory(int CategoryId)
		{
			using (var httpClient = new HttpClient())
			{
				using (var response = await httpClient.DeleteAsync("http://localhost:7000/api/category/" + CategoryId))
				{
					string apiRes = await response.Content.ReadAsStringAsync();
				}
			}
			return RedirectToAction("IndexCategory");
		}
	}
}
