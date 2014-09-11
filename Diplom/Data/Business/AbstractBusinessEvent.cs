using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Data.Business
{
    /// <summary>
    /// Абстрактное бизнес-событие предметной области
    /// </summary>
    abstract class AbstractBusinessEvent
    {
        /// <summary>
        /// Тип события
        /// </summary>
        protected String typeOfEvent = "";

        /// <summary>
        /// Время наступления события
        /// </summary>
        protected double eventTime = 0;

        /// <summary>
        /// Бизнес-процесс, в котором наступило событие
        /// </summary>
        protected AbstractBusinessProcess businessProcess;

        public void setType(String type)
        {
            this.typeOfEvent = type;
        }

        public void setTime(double time)
        {
            this.eventTime = time;
        }

        public void setBusinessProcess(AbstractBusinessProcess iBusinessProcess)
        {
            this.businessProcess = iBusinessProcess;
        }

        public String getType()
        {
            return typeOfEvent;
        }

        public double getTime()
        {
            return eventTime;
        }

        public AbstractBusinessProcess getBusinessProcess()
        {
            return businessProcess;
        }
    }
}
