using Photon.Pun;
using TMPro;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private float speed;
    [SerializeField] private PhotonView photonView;
    [SerializeField] private new TextMeshPro name;

    private bool canMovePlayer;
    private Vector3 playerDirection;
    private new Rigidbody rigidbody;
    #endregion

    #region UNITY CALLBACKS
    void Awake()
    {
        canMovePlayer = false;
        rigidbody = transform.GetComponent<Rigidbody>();
        name.text = Server.Instance.GetNickName(photonView);
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                playerDirection = Vector3.up;
                canMovePlayer = true;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                playerDirection = Vector3.down;
                canMovePlayer = true;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                playerDirection = Vector3.left;
                canMovePlayer = true;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                playerDirection = Vector3.right;
                canMovePlayer = true;
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                canMovePlayer = false;
            }

            if (canMovePlayer)
            {
                transform.Translate(playerDirection * Time.deltaTime * speed);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        rigidbody.isKinematic = true;
        collision.gameObject.SetActive(false);
        rigidbody.isKinematic = false;

        UpdatePlayerScore();
    }
    #endregion

    private void UpdatePlayerScore()
    {
        if (photonView.IsMine)
        {
            int score = int.Parse(Server.Instance.GetCustomProperty("score"));
            score++;

            // Update the player's score property
            Server.Instance.UpdateCustomProperty("score", score.ToString());
        }
    }
}