using UnityEngine;


namespace WW.CustomPhysics {
    /// <summary>
    /// Desc: This class is used to handle the grab logic/physics
    ///       in relation to grabbing the item with the claw.
    ///       It simply checks, using trigger enter, if something
    ///       has entered the zone (which is a small circle in the center
    ///       of the claw). If it has, check if we are going to be
    ///       nice enough to have it succeed (Just like a real claw machine).
    ///       
    /// Author: Joel Gabriel
    /// </summary>
    public class ClawMachineClaw : MonoBehaviour {

        [SerializeField] private ClawMachineController m_clawMachineController;
        [SerializeField] private int m_randResult;      
        [SerializeField] [Range (0, 100)] private int m_percentChanceOfGrab;

        private void OnTriggerEnter ( Collider col ) {
            // HACK: massive conditional check, pretty ugly but gets the job done
            if (col.tag != "NoInteractionZone" && 
                col.tag != "HatAttach" && 
                col.tag != "Player" &&
                col.tag != "CigarPoint" &&
                col.tag != "FacialFeature" &&
                col.tag != "MainCamera") 
            {
                // Let the claw machine controller know that there is an item in the zone.
                m_clawMachineController.HasItem = true;

                Debug.Log (col.tag);

                // Set the random result int to be a number between 0, 100.
                m_randResult = Random.Range (0, 100);

                // If we are less than the percent chance of grab (eg 70%, if the number is less than 70).
                if (m_randResult < m_percentChanceOfGrab) {
                    // Check what we are grabbing is actually something it can pickup.
                    if (m_clawMachineController.GrabbedItem == null)
                        // Give the claw machine controller the grabbed item.
                        m_clawMachineController.GrabbedItem = col.GetComponent<Rigidbody> ();
                }
            }
        }
    }
}