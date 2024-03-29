﻿using Application.Abstracts.Repositories;

namespace Application.Abstracts.Common.Interfaces;

public interface IUnitOfWork
{
    ICategoryRepository CategoryRepository { get; }
    IMediaRepository MediaRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    IPortfolioImageRepository PortfolioImageRepository { get; }
    IPortfolioRepository PortfolioRepository { get; }
    ITeamRepository TeamRepository { get; }
    IOurValueRepository OurValueRepository { get; }
    IFooterRepository FooterRepository { get; }
    IContactRepository ContactRepository { get; }
    IHomeRepository HomeRepository { get; }
    IEscRepository EscRepository { get; }
    ILoveRepository LoveRepository { get; }
    IWeRepository WeRepository { get; }
    IEmployeesPageRepository EmployeesPageRepository { get; }
    IBlogRepository BlogRepository { get; }
    IBlogTagCloudRepository BlogTagCloudRepository { get; }
    ITagRepository TagRepository { get; }
    IBlogMetaRepository BlogMetaRepository { get; }
    IHeaderRepository HeaderRepository { get; }
    IAwardRepository AwardRepository { get; }
    ICounterRepository CounterRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
