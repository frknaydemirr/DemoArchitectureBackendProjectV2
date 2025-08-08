using Castle.DynamicProxy;
using Core.CrossCuttinsConcerns.Validations;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Validation
{
  //bu metodu çalıştırmadan önce çalıştıracağım
    public  class ValidationAspect : MethodInterception
    {

        private Type _validatorType;

        public ValidationAspect(Type validatorType)
        {
            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
           var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType=_validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }

    }
}
