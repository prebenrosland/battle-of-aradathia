using UnityEngine;

namespace AS
{
    public class NewGoldPickUp : Interactable
    {
        public int goldAmount;

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);

            PickUpGold(playerManager);
        }

        private void PickUpGold(PlayerManager playerManager)
        {
            Player player = playerManager.GetComponent<Player>();
            player.PickUpGold(goldAmount);

            PlayerLocomotion playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
            AnimatorHandler animatorHandler = playerManager.GetComponentInChildren<AnimatorHandler>();

            playerLocomotion.rigidbody.velocity = Vector3.zero;
            animatorHandler.PlayTargetAnimation("Picking Up", true); 
            Destroy(gameObject);
        }
    }
}
