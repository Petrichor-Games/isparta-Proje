using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swipe : MonoBehaviour
{
    

        public SwipeManager swipeControls;
        public Transform Player;
        private Vector3 desiredPosition;

        private void Update()
        {
            if (swipeControls.SwipeLeft)
                desiredPosition += Vector3.left;
            if (swipeControls.SwipeRight)
                desiredPosition += Vector3.right;

            Player.transform.position = Vector3.MoveTowards
                (Player.transform.position, desiredPosition, 0.5f * Time.deltaTime);
        }
}
