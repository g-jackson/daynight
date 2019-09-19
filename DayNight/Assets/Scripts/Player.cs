using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public bool isMoving = false;
    public bool onCooldown = false;
    private float moveTime = 0.1f;
    private GameObject GroundTiles;
    private GameObject Props;
    public Tilemap groundTilemap;
    public Tilemap propsTilemap;
    private Vector2 direction = new Vector2(1, 0);
    private Animator animator;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update ()
    {
        //We do nothing if the player is still moving.
        if (isMoving || onCooldown){
            return;
        }
        //To store move directions.
        int horizontal = 0;
        int vertical = 0;
        // WASD Controls
        if (Input.GetKey("w")){
          horizontal = 1;
        }
        else if(Input.GetKey("s")){
          horizontal = -1;
        }
        else if(Input.GetKey("a")){
          vertical = -1;
        }
        else if(Input.GetKey("d")){
          horizontal = 1;
        }
        // Arrow key Controls
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        //Prevent going in both directions at the same time
        if ( horizontal != 0 )
            vertical = 0;

        //If there's a direction, we are trying to move.
        if (horizontal != 0 || vertical != 0)
        {
            StartCoroutine(actionCooldown(moveTime));
            Move(horizontal, vertical);
        }
        else if(Input.GetKey("e")){
            StartCoroutine(actionCooldown(moveTime));
            Interact();
        }
    }


    private void Interact()
    {
      Vector2 startCell =  transform.position;
      Vector2 tarGetCell = startCell + CartesianToIsometric(direction);
      Collider2D coll = CheckTile(tarGetCell);
      if (coll != null){
          if (coll.GetComponent("Interactable")){
              coll.gameObject.GetComponent<Interactable>().Interact();
          }
      }
      else if (GetCell(propsTilemap, tarGetCell)){
          print("Interacting with non-interactable");
      }
      else{
          print("Nothing Found");
      }

    }


    private void Move(int xDir, int yDir)
    {
        direction = new Vector2(xDir, yDir);

        Vector2 startCell =  transform.position;
        Vector2 tarGetCell = startCell + CartesianToIsometric(direction);
        Collider2D coll = CheckTile(tarGetCell);
        //print("movedir:"+ xDir + yDir + "start:" + startCell + "end:" + tarGetCell + "new_dir:" + direction);
        if (coll != null){
            StartCoroutine(BlockedMovement(tarGetCell, xDir, yDir));
        }
        else if (GetCell(propsTilemap, tarGetCell) || !GetCell(groundTilemap, tarGetCell)){
            StartCoroutine(BlockedMovement(tarGetCell, xDir, yDir));
        }
        else if (GetCell(groundTilemap, tarGetCell)){
            StartCoroutine(SmoothMovement(tarGetCell, xDir, yDir));
        }
    }


    private void TriggerPlayerAnimtor(int xDir, int yDir){
        if (xDir == 0 && yDir == 1){
            animator.SetInteger("state", 1);
        }
        else if (xDir == 0 && yDir == -1){
            animator.SetInteger("state", 4);
        }
        else if (xDir == 1 && yDir == 0){
            animator.SetInteger("state", 3);
        }
        else if (xDir == -1 && yDir == 0){
            animator.SetInteger("state", 2);
        }
    }


    private void StopPlayerAnimtor()
    {
        animator.SetInteger("state", -1);
    }


    private IEnumerator SmoothMovement(Vector3 end, int xDir, int yDir)
    {
        isMoving = true;
        TriggerPlayerAnimtor(xDir, yDir);
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        float inverseMoveTime = 1 / moveTime;

        while (sqrRemainingDistance > float.Epsilon){
            Vector3 newPosition = Vector3.MoveTowards(transform.position, end, inverseMoveTime * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }
        StopPlayerAnimtor();
        isMoving = false;
    }


    private IEnumerator BlockedMovement(Vector3 end, int xDir, int yDir)
    {
        //while (isMoving) yield return null;

        isMoving = true;
        TriggerPlayerAnimtor(xDir, yDir);
        Vector3 originalPos = transform.position;

        end = transform.position + ((end - transform.position) / 4);
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        float inverseMoveTime = (1 / (moveTime*2) );

        while (sqrRemainingDistance > float.Epsilon){
            Vector3 newPosition = Vector3.MoveTowards(transform.position, end, inverseMoveTime * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }

        sqrRemainingDistance = (transform.position - originalPos).sqrMagnitude;
        while (sqrRemainingDistance > float.Epsilon){
            Vector3 newPosition = Vector3.MoveTowards(transform.position, originalPos, inverseMoveTime * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistance = (transform.position - originalPos).sqrMagnitude;

            yield return null;
        }
        StopPlayerAnimtor();
        isMoving = false;
    }


    private IEnumerator actionCooldown(float cooldown)
    {
        onCooldown = true;
        while ( cooldown > 0f ){
            cooldown -= Time.deltaTime;
            yield return null;
        }
        onCooldown = false;
    }


    Vector2 CartesianToIsometric(Vector2 cartPt)
    {
        Vector2 tempPt = new Vector2();
        tempPt.x = cartPt.x - cartPt.y;
        tempPt.y = (cartPt.x + cartPt.y) / 2;
        return (tempPt);
    }


    Vector2 IsometricToCartesian(Vector2 isoPt)
    {
      Vector2 tempPt = new Vector2();
      tempPt.x = (2 * isoPt.y + isoPt.x) / 2;
      tempPt.y = (2 * isoPt.y - isoPt.x) / 2;
      return (tempPt);
    }


    public Collider2D CheckTile(Vector2 targetPos)
    {
        RaycastHit2D hit;
        hit = Physics2D.Linecast(targetPos, targetPos);
        return hit.collider;
    }


    private TileBase GetCell(Tilemap tilemap, Vector2 cellWorldPos)
    {
        return tilemap.GetTile(tilemap.WorldToCell(cellWorldPos));
    }


    private bool HasTile(Tilemap tilemap, Vector2 cellWorldPos)
    {
        return tilemap.HasTile(tilemap.WorldToCell(cellWorldPos));
    }
}
