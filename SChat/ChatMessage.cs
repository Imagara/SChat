//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SChat
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChatMessage
    {
        public int Id { get; set; }
        public int IdChat { get; set; }
        public int IdMessage { get; set; }
    
        public virtual Chat Chat { get; set; }
        public virtual Message Message { get; set; }
    }
}
