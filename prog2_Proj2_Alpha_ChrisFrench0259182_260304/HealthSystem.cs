using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog2_Proj2_Alpha_ChrisFrench0259182_260304
{
    public class HealthSystem
    {
        public int _health { get; set; }
        public bool _isDead => _health <= 0;// defines dead state

        public HealthSystem(int maxHP) => _health = maxHP; // sets max health

        public void TakeDamage(int amount) => _health -= amount;// defines damage 
    }



}

