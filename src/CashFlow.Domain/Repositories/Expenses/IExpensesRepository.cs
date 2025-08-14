﻿using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses
{
    public interface IExpensesRepository
    {
        Task Add(Expense expense);
        Task<List<Expense>> GetAll();
        Task<Expense?> GetById(long id);

        /// <summary>
        /// This method returns true if the expense was deleted, false if not found.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(long id);
    }
}
