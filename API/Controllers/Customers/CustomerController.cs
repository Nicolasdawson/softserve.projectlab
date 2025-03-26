using API.Models.Customers;
using API.Services.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Customers;

/// <summary>
/// API controller for managing customers.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly ICartService _cartService;
    private readonly IPackageService _packageService;

    /// <summary>
    /// Initializes a new instance of the CustomersController class.
    /// </summary>
    /// <param name="customerService">The customer service.</param>
    /// <param name="cartService">The cart service.</param>
    /// <param name="packageService">The package service.</param>
    public CustomersController(
        ICustomerService customerService,
        ICartService cartService,
        IPackageService packageService)
    {
        _customerService = customerService;
        _cartService = cartService;
        _packageService = packageService;
    }

    /// <summary>
    /// Gets all customers.
    /// </summary>
    /// <returns>A collection of all customers.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    /// <summary>
    /// Gets a customer by ID.
    /// </summary>
    /// <param name="id">The ID of the customer to retrieve.</param>
    /// <returns>The customer if found; otherwise, NotFound.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Customer>> GetCustomerById(string id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null)
        {
            return NotFound();
        }
        return Ok(customer);
    }

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="customer">The customer to create.</param>
    /// <returns>The created customer.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
    {
        var createdCustomer = await _customerService.CreateCustomerAsync(customer);
        return CreatedAtAction(nameof(GetCustomerById), new { id = createdCustomer.Id }, createdCustomer);
    }

    /// <summary>
    /// Updates an existing customer.
    /// </summary>
    /// <param name="id">The ID of the customer to update.</param>
    /// <param name="customer">The updated customer information.</param>
    /// <returns>No content if the customer was updated; otherwise, NotFound.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCustomer(string id, Customer customer)
    {
        var updatedCustomer = await _customerService.UpdateCustomerAsync(id, customer);
        if (updatedCustomer == null)
        {
            return NotFound();
        }
        return NoContent();
    }

    /// <summary>
    /// Deletes a customer.
    /// </summary>
    /// <param name="id">The ID of the customer to delete.</param>
    /// <returns>No content if the customer was deleted; otherwise, NotFound.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCustomer(string id)
    {
        var result = await _customerService.DeleteCustomerAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    /// <summary>
    /// Gets a customer's line of credit.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <returns>The customer's line of credit if found; otherwise, NotFound.</returns>
    [HttpGet("{customerId}/credit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LineOfCredit>> GetLineOfCredit(string customerId)
    {
        var lineOfCredit = await _customerService.GetLineOfCreditAsync(customerId);
        if (lineOfCredit == null)
        {
            return NotFound();
        }
        return Ok(lineOfCredit);
    }

    /// <summary>
    /// Creates a line of credit for a customer.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <param name="lineOfCredit">The line of credit to create.</param>
    /// <returns>The created line of credit.</returns>
    [HttpPost("{customerId}/credit")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LineOfCredit>> CreateLineOfCredit(string customerId, LineOfCredit lineOfCredit)
    {
        try
        {
            var createdLineOfCredit = await _customerService.CreateLineOfCreditAsync(customerId, lineOfCredit);
            return CreatedAtAction(nameof(GetLineOfCredit), new { customerId }, createdLineOfCredit);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Gets a customer's cart.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <returns>The customer's cart.</returns>
    [HttpGet("{customerId}/cart")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Cart>> GetCart(string customerId)
    {
        try
        {
            var cart = await _cartService.GetOrCreateCartForCustomerAsync(customerId);
            return Ok(cart);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Adds an item to a customer's cart.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <param name="itemId">The ID of the item to add.</param>
    /// <param name="quantity">The quantity to add.</param>
    /// <returns>The updated cart.</returns>
    [HttpPost("{customerId}/cart/items")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Cart>> AddItemToCart(string customerId, [FromQuery] string itemId, [FromQuery] int quantity)
    {
        try
        {
            var cart = await _cartService.GetOrCreateCartForCustomerAsync(customerId);
            var updatedCart = await _cartService.AddItemToCartAsync(cart.Id, itemId, quantity);
            return Ok(updatedCart);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Converts a customer's cart to a package.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <param name="packageName">The name for the new package.</param>
    /// <param name="contractTermMonths">The contract term in months.</param>
    /// <returns>The created package.</returns>
    [HttpPost("{customerId}/cart/checkout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Package>> CheckoutCart(
        string customerId,
        [FromQuery] string packageName,
        [FromQuery] int contractTermMonths)
    {
        try
        {
            var cart = await _cartService.GetOrCreateCartForCustomerAsync(customerId);
            var package = await _cartService.ConvertCartToPackageAsync(cart.Id, packageName, contractTermMonths);
            
            if (package == null)
            {
                return BadRequest("Cart is empty or could not be converted to a package");
            }
            
            return Ok(package);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Gets all packages for a customer.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <returns>A collection of packages for the customer.</returns>
    [HttpGet("{customerId}/packages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Package>>> GetCustomerPackages(string customerId)
    {
        var packages = await _packageService.GetPackagesByCustomerIdAsync(customerId);
        return Ok(packages);
    }

    /// <summary>
    /// Gets a specific package for a customer.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <param name="packageId">The ID of the package to retrieve.</param>
    /// <returns>The package if found; otherwise, NotFound.</returns>
    [HttpGet("{customerId}/packages/{packageId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Package>> GetCustomerPackage(string customerId, string packageId)
    {
        var package = await _packageService.GetPackageByIdAsync(packageId);
        
        if (package == null || package.Customer.Id != customerId)
        {
            return NotFound();
        }
        
        return Ok(package);
    }

    /// <summary>
    /// Searches for customers based on search criteria.
    /// </summary>
    /// <param name="searchTerm">The search term to match against customer properties.</param>
    /// <returns>A collection of customers matching the search criteria.</returns>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Customer>>> SearchCustomers([FromQuery] string searchTerm)
    {
        var customers = await _customerService.SearchCustomersAsync(searchTerm);
        return Ok(customers);
    }
}