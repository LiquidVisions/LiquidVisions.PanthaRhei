﻿using System;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Application.Mappers;
using LiquidVisions.PanthaRhei.Generated.Client.RequestModels.Fields;
using LiquidVisions.PanthaRhei.Generated.Application.Validators;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generated.Application.Interactors.Fields
{
    internal class UpdateFieldInteractor : IInteractor<UpdateFieldCommand>
    {
        private readonly IValidator<UpdateFieldCommand> validator;
        private readonly IMapper<UpdateFieldCommand, Field> mapper;
        private readonly IUpdateGateway<Field> repository;
        private readonly IGetByIdGateway<Field> getRepository;

        public UpdateFieldInteractor(
            IValidator<UpdateFieldCommand> validator,
            IMapper<UpdateFieldCommand, Field> mapper,
            IUpdateGateway<Field> repository,
            IGetByIdGateway<Field> getRepository)
        {
            this.validator = validator;
            this.mapper = mapper;
            this.repository = repository;
            this.getRepository = getRepository;
        }

        public async Task<Response> ExecuteUseCase(UpdateFieldCommand requestModel)
        {
            Response response = validator.Validate(requestModel);
            if (response.IsValid)
            {
                try
                {
                    Field entity = getRepository.GetById(requestModel.Id);
                    if (entity == null)
                    {
                        response.AddError(ErrorCodes.NotFound, $"Resource {nameof(Field)} with id {requestModel.Id} not found.");
                        return response;
                    }

                    mapper.Map(requestModel, entity);
                    response.SetParameter(entity);

                    int repositoryResult = await repository.Update(entity);
                    if (repositoryResult != 1)
                    {
                        response.AddError(ErrorCodes.InternalServerError, $"Failed to create {nameof(Field)}.");
                        return response;
                    }
                }
                catch (Exception exception)
                {
                    response.AddError(ErrorCodes.InternalServerError, exception.Message);
                }
            }

            return response;
        }
    }
}
