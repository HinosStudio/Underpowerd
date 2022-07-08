using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Messaging {
    public enum MessageType {

    }

    public class Telegram {
        public GameObject sender;
        public GameObject receiver;
        public MessageType type;
        public DateTime DispatchTime;
    }


}
