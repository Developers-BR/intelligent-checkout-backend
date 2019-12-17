using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IntelligentCheckout.Backend.Infra
{
    public static class CustomModelStateDictionary
    {
        public static Models.Resposta<T> ObterResposta<T>(this ModelStateDictionary modelStateDictionary)
            where T : class
        {
            if (modelStateDictionary.IsValid) return null;

            var resposta = new Models.Resposta<T>(default);
            foreach (var item in modelStateDictionary)
                foreach (var erro in item.Value.Errors)
                    resposta.Avisos.Add(new Models.Aviso(item.Key, erro.ErrorMessage));

            return resposta;
        }
    }
}
