using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // VARIABLE DE VITESSE DE MOUVEMENT (VALEUR A DEFINIR DANS UNITY) 
    public float moveSpeed;

    // VARIABLE DE FORCE DE SAUT (VALEUR A DEFINIR DANS UNITY)
    public float jumpForce;

    // VARIABLE STOCKANT L'ACTION DE SAUT
    public bool isJumping;

    // VARIABLE STOCKANT L'ACTION D'ETRE AU SOL
    public bool isGrounded;

    // VARIABLES CORRESPONDANT A UN POINT A DEFINIR DANS UNITY, PERMETTANT DE SAVOIR
    // LORSQUE LE JOUEUR TOUCHE LE SOL
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;
    
    public Rigidbody2D rb;
    public Animator animator;

    // RENDU VISUEL DU JOUEUR
    public SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;

    void Update()
    {   
        // SI LA BARRE ESPACE EST PRESSEE ET QUE LE JOUEUR EST AU SOL
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // LE JOUEUR SAUTE
            isJumping = true;
        }

        // FONCTION QUI TOURNE LE JOUEUR SUR LUI-MEME
        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
    }

    void FixedUpdate()
    {
        // VARIABLE FLOAT QUI VA ENREGISTRER LE MOUVEMENT HORIZONTAL GRACE A LA PRESSION 
        // SUR LES FLECHES DIRECTIONNELLES GAUCHE ET DROITE MULTIPLIE PAR LA VITESSE DU MOUVEMENT ET LE TEMPS
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        // PERMET DE CREER UNE ZONE ENTRE LES DEUX POINTS
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);

        // FONCTION POUR BOUGER LE JOUEUR PRENANT EN PARAMETRE LES MOUVEMENTS HORIZONTAUX
        MovePlayer(horizontalMovement);
    }
    // CREATION DE LA FONCTION POUR BOUGER LE JOUEUR PRENANT EN PARAMETRE LES MOUVEMENTS HORIZONTAUX
    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        // SI LE JOUEUR SAUTE
        if (isJumping == true)
        {
            // AFFECTER UNE FORCE DANS L'AXE Y DU JOUEUR (VALEUR VARIABLE JUMP FORCE A DEFINIR DANS UNITY)
            rb.AddForce(new Vector2(0f, jumpForce));

            // DESACTIVER LE SAUT DU JOUEUR
            isJumping = false;
        }
    }
    // CREATION DE LA FONCTION POUR TOURNER LE JOUEUR SUR LUI-MEME
    void Flip(float _velocity)
    {
        if(_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }else if(_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
