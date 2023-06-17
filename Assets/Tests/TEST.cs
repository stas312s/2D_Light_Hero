using NUnit.Compatibility;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
namespace Tests.Player.Edit_Mode
{
    public class PlayerMovementTests
    {
        private GameObject playerObject;
        private Player_Behaviour playerController;
        
        [OneTimeSetUp]
        
        public void Setup()
        {
            playerController = new Player_Behaviour();
            playerController.Awake();
        }
        [Test]
        public void iwantdead()
        {
            for (int i = 0; i < 5; i++)
            {
                playerController.GetDamage();
            }

            Assert.That(playerController.IsDead, Is.EqualTo(true)); 

        }
        [Test]
        public void inotwantdead()
        {
            for (int i = 0; i < 3; i++)
            {
                playerController.GetDamage();
            }

            Assert.That(playerController.IsDead, Is.EqualTo(false)); 

        }

        [Test]
        public void getdamage()
        {
            var lives = playerController.Lives;
            playerController.GetDamage();
            Assert.That(playerController.Lives, Is.EqualTo(lives-1)); 

        }

        [Test]
        public void TestMoneyPickup()
        {
            var oleg = new GameObject("Player");
            oleg.AddComponent<BoxCollider2D>();
            var bonusObject = new GameObject().AddComponent<Bonus>();
            bonusObject.bonusName = "Money";
            PlayerPrefs.SetInt("Money", 0);
            bonusObject.OnTriggerEnter2D(oleg.GetComponent<BoxCollider2D>());
            Assert.AreEqual(1, PlayerPrefs.GetInt("Money"));
            Object.DestroyImmediate(bonusObject.gameObject);
            Object.DestroyImmediate(oleg);
        }

        [Test]
        public void TestPotionPickup()
        {
            var oleg = new GameObject("Player");
            oleg.AddComponent<BoxCollider2D>();
            var bonusObject = new GameObject().AddComponent<Bonus>();
            bonusObject.bonusName = "Potion";
            bonusObject.OnTriggerEnter2D(oleg.GetComponent<BoxCollider2D>());
            Assert.IsTrue(Player_Behaviour.Instance.HealthPotionCalled);
            Object.DestroyImmediate(bonusObject.gameObject);
            Object.DestroyImmediate(oleg);
        }

        [Test]
        public void MaxSpeed()
        {
            float expectedMaxSpeed = 4f;
            Assert.AreEqual(expectedMaxSpeed, playerController.maxSpeed);
            float newMaxSpeed = 1f;
            playerController.maxSpeed = newMaxSpeed;
            Assert.AreEqual(newMaxSpeed, playerController.maxSpeed);
        }
        [Test]
        public void TestHeartsArray()
        {
            int lives = 3;
            GameObject[] hearts = new GameObject[5];
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i] = new GameObject();
            }
            for (int i = 0; i < lives; i++)
            {
                hearts[i].SetActive(false);
            }
            for (int i = 0; i < lives; i++)
            {
                Assert.IsFalse(hearts[i].activeSelf);
            }
            for (int i = lives; i < hearts.Length; i++)
            {
                Assert.IsTrue(hearts[i].activeSelf);
            }
        }
        
       
        

    }

}