using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite.Presentation.Events;

namespace SA.Infrastructure.Event
{
    public class EntityChangedEvent : CompositePresentationEvent<IEntityChangedEventArgs>
    {
    }
}
