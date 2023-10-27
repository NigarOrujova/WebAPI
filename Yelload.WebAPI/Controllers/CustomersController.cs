﻿using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Commands.DeleteCustomer;
using Application.Customers.Commands.UpdateCustomer;
using Application.Customers.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers;

public class CustomersController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
  => Ok(await Mediator.Send(new CustomerSingleQuery(id)));
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
   => Ok(await Mediator.Send(new CustomerAllQuery()));
    [HttpGet("paginate")]
    public async Task<ActionResult<List<Customer>>> GetCustomers(int page = 1, int size = 10)
    {
        var query = new CustomersWithPaginationQuery { Page = page, Size = size };
        var result = await Mediator.Send(query);

        return Ok(result);
    }
    [HttpPost]
    [Authorize(Policy = "admin.customers.post")]
    public async Task<IActionResult> CreateAsync(CreateCustomerCommand request)
   => Ok(await Mediator.Send(request));
    [HttpPut]
    [Authorize(Policy = "admin.customers.put")]
    public async Task<IActionResult> UpdateAsync(UpdateCustomerCommand request)
   => Ok(await Mediator.Send(request));
    [HttpDelete("{id}")]
    [Authorize(Policy = "admin.customers.delete")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
   => Ok(await Mediator.Send(new DeleteCustomerCommand(id)));
}
