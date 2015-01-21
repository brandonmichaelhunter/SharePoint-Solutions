using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace HomeworkTrackingSystemPrj.Utility
{
    public class DisabledEventsScope : SPItemEventReceiver, IDisposable
    {
        // Boolean to hold the original value of the EventFiringEnabled property 
        bool _originalValue;

        public DisabledEventsScope()
        {
            // Save off the original value of EventFiringEnabled 
            _originalValue = base.EventFiringEnabled;

            // Set EventFiringEnabled to false to disable it 
            base.EventFiringEnabled = false;
        }

        public void Dispose()
        {
            // Set EventFiringEnabled back to its original value 
            base.EventFiringEnabled = _originalValue;
        }
    }
}
